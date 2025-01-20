using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/* Simple Physics Toolkit - Magnet
 * Description: Magnet applies force to any rigidbody within range
 * Required Components: None
 * Author: Dylan Anthony Auty
 * Version: 1.4
 * Last Change: 2020-12-03
*/

namespace SimplePhysicsToolkit
{
	public class BuMagnet : MonoBehaviour
	{
		public float magnetForce = 15.0f;
		public static bool enable = true;
		public static bool attract = true;
		public float innerRadius = 2.0f;
		public float outerRadius = 5.0f;



		public static int a = 0;

		public bool useColliderAsTrigger = false;
		public bool onlyAffectInteractableItems = false;
		public bool realismMode = false;

		public ColliderEvent onAffect;
		public Material[] mat = new Material[2];
		private Transform tr;
		public GameObject Pull;
		public GameObject Push;

		private GameObject currentEffect;

		void Start()
		{
			tr = GetComponent<Transform>();
			StartCoroutine(BuChange());
		}

		/*void Update(){
				if (attract == true){
					
					GameObject effect = Instantiate(Pull, tr.position, transform.rotation);
					Destroy(effect, ChangCount);
					gameObject.GetComponent<MeshRenderer>().material = mat[0];
					yield return new WaitForSeconds(ChangCount);

			}
				else{
					
					GameObject effect2 = Instantiate(Push,tr.position,transform.rotation);
					Destroy(effect2, ChangCount);
					gameObject.GetComponent<MeshRenderer>().material = mat[1];

			}
		}*/

		void FixedUpdate()
		{
			if (enable)
			{
				if (!useColliderAsTrigger)
				{
					Collider[] objects = Physics.OverlapSphere(transform.position, outerRadius);
					foreach (Collider col in objects)
					{
						if (col.GetComponent<Rigidbody>())
						{ //Must be rigidbody
							if (onlyAffectInteractableItems)
							{
								if (col.GetComponent<InteractableItem>())
								{
									attractOrRepel(col);
								}
							}
							else
							{
								attractOrRepel(col);
							}
						}
					}
				}
				else
				{
					/* The user has opted to use a custom collider as a trigger, we don't need to do anything as this is handled by magic methods */
				}

			}
		}

		void OnTriggerStay(Collider col)
		{
			if (useColliderAsTrigger)
			{
				if (col.GetComponent<Rigidbody>())
				{
					if (onlyAffectInteractableItems)
					{
						if (col.GetComponent<InteractableItem>())
						{
							attractOrRepel(col);
						}
					}
					else
					{
						attractOrRepel(col);
					}
				}
			}
		}

		void attractOrRepel(Collider col)
		{// 끌꺼나 민다
			if (!useColliderAsTrigger)
			{
				if (Vector3.Distance(transform.position, col.transform.position) > innerRadius)
				{
					//Apply force in direction of magnet center
					if (realismMode)
					{
						float dynamicDistance = Mathf.Abs((Vector3.Distance(transform.position, col.transform.position)) - (outerRadius + (innerRadius * 2)));
						float multiplier = dynamicDistance / outerRadius;

						if (attract)
						{
							col.GetComponent<Rigidbody>().AddForce((magnetForce * (transform.position - col.transform.position).normalized) * multiplier, ForceMode.Force);
						}
						else
						{
							col.GetComponent<Rigidbody>().AddForce(-(magnetForce * (transform.position - col.transform.position).normalized) * multiplier, ForceMode.Force);
						}
					}
					else
					{
						if (attract)
						{
							col.GetComponent<Rigidbody>().AddForce(magnetForce * (transform.position - col.transform.position).normalized, ForceMode.Force);
						}
						else
						{
							col.GetComponent<Rigidbody>().AddForce(-magnetForce * (transform.position - col.transform.position).normalized, ForceMode.Force);
						}
					}

				}
				else
				{
					//Inner Radius float gentle - Future additional handling here
				}
			}
			else
			{
				/* Handles as part of the new collider logic
				 * 
				 * This system does not need to check inner radius as it does not apply
				 * 
				 * Date: 2021-12-22
				*/
				Collider trigger = GetComponent<Collider>();
				if (realismMode)
				{
					float longestSide = Mathf.Max(trigger.bounds.size.x, trigger.bounds.size.y);
					longestSide = Mathf.Max(longestSide, trigger.bounds.size.z);

					float dynamicDistance = Mathf.Abs((Vector3.Distance(transform.position, col.transform.position)) - (longestSide));
					float multiplier = dynamicDistance / longestSide;

					if (attract)
					{
						col.GetComponent<Rigidbody>().AddForce((magnetForce * (transform.position - col.transform.position).normalized) * multiplier, ForceMode.Force);
					}
					else
					{
						col.GetComponent<Rigidbody>().AddForce(-(magnetForce * (transform.position - col.transform.position).normalized) * multiplier, ForceMode.Force);
					}
				}
				else
				{
					if (attract)
					{
						col.GetComponent<Rigidbody>().AddForce(magnetForce * (transform.position - col.transform.position).normalized, ForceMode.Force);
					}
					else
					{
						col.GetComponent<Rigidbody>().AddForce(-magnetForce * (transform.position - col.transform.position).normalized, ForceMode.Force);
					}
				}

			}
			onAffect.Invoke(col);
		}

		void OnDrawGizmos()
		{
			if (enable)
			{
				if (!useColliderAsTrigger)
				{
					Gizmos.color = Color.red;
					Gizmos.DrawWireSphere(transform.position, outerRadius);
					Gizmos.color = Color.yellow;
					Gizmos.DrawWireSphere(transform.position, innerRadius);

					Gizmos.DrawIcon(transform.position, "sptk_magnet.png", true);
				}
			}
		}

		IEnumerator BuChange()
		{
			while (true)
			{
				if (attract == false)
				{
					gameObject.GetComponent<MeshRenderer>().material = mat[0];
					GameObject effect = Instantiate(Pull, tr.position, transform.rotation);
					yield return new WaitUntil(() => attract == true); // attract이 true가 될 때까지 대기
					Destroy(effect);

				}
				else
				{
					gameObject.GetComponent<MeshRenderer>().material = mat[1];
					GameObject effect2 = Instantiate(Push, tr.position, transform.rotation);
					yield return new WaitUntil(() => attract == false); // attract이 false가 될 때까지 대기
					Destroy(effect2);

				}
			}
		}

	}
}
