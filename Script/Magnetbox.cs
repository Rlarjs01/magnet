using UnityEngine;
using UnityEngine.Events;

namespace SimplePhysicsToolkit {
    public class Magnetbox : MonoBehaviour {
        public float magnetForce = 15.0f;
        public static bool enable = true;
        public bool attract = true;
        public float innerRadius = 2.0f;
        public Vector3 outerBox = new Vector3(10f, 10f, 5f);
        public Vector3 innerBox = new Vector3(10f, 1f, 1f);

        public static int a = 0;

        public bool useColliderAsTrigger = false;
        public bool onlyAffectInteractableItems = false;
        public bool realismMode = false;

        public ColliderEvent onAffect;
        

private void Update() {
            
        }

        void FixedUpdate () {
            if (enable) {
                if (!useColliderAsTrigger) {
                    Collider[] objects = Physics.OverlapBox(transform.position, outerBox);
                    foreach (Collider col in objects) {
                        if (col.GetComponent<Rigidbody>()) { //Must be rigidbody
                            if (onlyAffectInteractableItems) {
                                if (col.GetComponent<InteractableItem>()) {
                                    attractOrRepel(col);
                                }
                            } else {
                                attractOrRepel(col);
                            }
                        }
                    }
                } else {
                    /* The user has opted to use a custom collider as a trigger, we don't need to do anything as this is handled by magic methods */
                }
            }
        }

        void OnTriggerStay(Collider col) {
            if (useColliderAsTrigger) {
                if (col.GetComponent<Rigidbody>()) {
                    if (onlyAffectInteractableItems) {
                        if (col.GetComponent<InteractableItem>()) {
                            attractOrRepel(col);
                        }
                    } else {
                        attractOrRepel(col);
                    }
                }
            }
        }

        void attractOrRepel(Collider col) {
            if (!useColliderAsTrigger) {
                if (Vector3.Distance(transform.position, col.transform.position) > innerBox.magnitude) {
                    //Apply force in direction of magnet center
                    if (realismMode) {
                        float dynamicDistance = Mathf.Abs((Vector3.Distance(transform.position, col.transform.position)) - (outerBox.magnitude + (innerBox.magnitude * 2)));
                        float multiplier = dynamicDistance / outerBox.magnitude;

                        if (attract) {
                            col.GetComponent<Rigidbody>().AddForce((magnetForce * (transform.position - col.transform.position).normalized) * multiplier, ForceMode.Force);
                        } else {
                            col.GetComponent<Rigidbody>().AddForce(-(magnetForce * (transform.position - col.transform.position).normalized) * multiplier, ForceMode.Force);
                        }
                    } else {
                        if (attract) {
                            col.GetComponent<Rigidbody>().AddForce(magnetForce * (transform.position - col.transform.position).normalized, ForceMode.Force);
                        } else {
                            col.GetComponent<Rigidbody>().AddForce(-magnetForce * (transform.position - col.transform.position).normalized, ForceMode.Force);
                        }
                    }

                } else {
                    //Inner Radius float gentle - Future additional handling here
                }
            } else {
                /* Handles as part of the new collider logic
                 * 
                 * This system does not need to check inner radius as it does not apply
                 * 
                 * Date: 2021-12-22
                */
                Collider trigger = GetComponent<Collider>();
                if (realismMode) {
                    float longestSide = Mathf.Max(trigger.bounds.size.x, trigger.bounds.size.y);
                    longestSide = Mathf.Max(longestSide, trigger.bounds.size.z);

                    float dynamicDistance = Mathf.Abs((Vector3.Distance(transform.position, col.transform.position)) - (longestSide));
                    float multiplier = dynamicDistance / longestSide;

                    if (attract) {
                        col.GetComponent<Rigidbody>().AddForce((magnetForce * (transform.position - col.transform.position).normalized) * multiplier, ForceMode.Force);
                    } else {
                        col.GetComponent<Rigidbody>().AddForce(-(magnetForce * (transform.position - col.transform.position).normalized) * multiplier, ForceMode.Force);
                    }
                } else {
                    if (attract) {
                        col.GetComponent<Rigidbody>().AddForce(magnetForce * (transform.position - col.transform.position).normalized, ForceMode.Force);
                    } else {
                        col.GetComponent<Rigidbody>().AddForce(-magnetForce * (transform.position - col.transform.position).normalized, ForceMode.Force);
                    }
                }
            }

            onAffect.Invoke(col);
        }

        void OnDrawGizmos() {
            Vector3 boxSizeL = outerBox;
            Vector3 boxSizeS = innerBox;

            if (enable) {
                if (!useColliderAsTrigger) {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireCube(transform.position, boxSizeL);
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireCube(transform.position, boxSizeS);

                    Gizmos.DrawIcon(transform.position, "sptk_magnet.png", true);
                }
            }
        }
    }
}