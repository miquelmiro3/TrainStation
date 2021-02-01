using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class NavMeshNavigator : MonoBehaviour
{

	private NavMeshAgent agent;
	public NavMeshPoints targets;
	public bool destroyWhenOnDestination;
	public bool idle;
	public float randomRadius;
	private float timeToConsiderStuck;
	private float stuckDistance;
	private float stuckTimer;
	private Vector3 stuckPosition;
	private bool panicking;
	private int idleChance;
	private float timeIdling;
	private float idleTimer;
	private bool alwaysIdle;

	// NEW
	public bool agenda = false;
	public bool destinationReached = false;
	public bool countDown = false;
	public bool working = false;
	public bool moveTo = false;
	public bool found = false;
	public float rotationSpeed = 1.5f;
	public bool makeRotation = false;
	public float yRotation = -1;
	public Tuple<Vector3, Vector3, string> obj;
	private bool turn = false;
	private bool stuck = false;
	private Animator animator;

	public delegate void AssignDestination(NavMeshNavigator nmn, string type, string id); //TEMA EVENTS
	public static event AssignDestination AssignPosition;

	public delegate void FreeDestination(string type, string id); //TEMA EVENTS
	public static event FreeDestination FreePosition;

	public bool rotate; // DEBUG
	public float walkSpeed;
	public float rotSpeed;

	public void ActivatePanicking() {
		panicking=true;
		idle=false;
	}

	public void SetDestination(Vector3 dest) {
		agent.SetDestination(dest);
	}

	public void SetSpeed(float speed) {
		agent.speed=speed;
	}

	private void CheckIfStuck() {
		if (Vector3.Distance(transform.position, stuckPosition)<stuckDistance) {
			if (stuckTimer>=timeToConsiderStuck) {
				stuckTimer=0;
				stuckPosition=transform.position;
				//Debug.Log("Stuck!!");
				if (agenda && moveTo && !stuck) stuck = true;
				GetOppositeDestination();
			}
			else stuckTimer+=Time.deltaTime;
		} else {
			stuckTimer=0;
			stuckPosition=transform.position;
		}
	}

	private Vector3 RandomNavmeshLocation(float radius) {
		Vector3 randomDirection = UnityEngine.Random.insideUnitSphere*radius;
		randomDirection+=transform.position;
		NavMeshHit hit;
		Vector3 finalPosition = Vector3.zero;
		if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
			finalPosition=hit.position;
		}
		return finalPosition;
	}

	public void SetRandomDestination() {
		//agent.SetDestination(targets.GetRandomPoint());
		if (agent) agent.SetDestination(RandomNavmeshLocation(randomRadius));
	}

	private void GetOppositeDestination() {
		Vector3 dest = agent.destination;
		//agent.SetDestination(new Vector3(-dest.x, dest.y, -dest.z));
		agent.SetDestination(transform.position-(transform.forward*3));
	}

	public void ObjectFound(Vector3 destination, Vector3 objective, string objectId) {
		obj = new Tuple<Vector3, Vector3, string>(destination, objective, objectId);
		found = true;

		SetDestinationToObject();// NOMES DOS ESTATS
	}

	public void SetDestinationToObject() {
		if (obj.Item1 == obj.Item2) {
			Vector3 dir = (transform.position - obj.Item2).normalized;
			Vector3 aux = obj.Item2 + dir * 0.1f;
			obj = new Tuple<Vector3, Vector3, string>(aux, obj.Item2, obj.Item3);
		}
		agent.SetDestination(obj.Item1);
		moveTo = true;
		makeRotation = true;
	}

	/*public void GetDestinationFromObject(string objectType, string objectId) {
		AssignPosition.Invoke(this, objectType, objectId);
		found = true;
	}

	public void SetDestinationToObject(Vector3 dest, Vector3 obj, string objectId) {
		Debug.Log(objectId);
		Debug.Log(dest);
		yRotation = -1;
		SetDestination(dest);
		id = objectId;
		Debug.Log(agent.destination);
	}*/

	public void SetFreeObject() {
		string type = obj.Item3.Split('_')[0];
		FreePosition.Invoke(type, obj.Item3);
	}

	public void Reset() {
		agent.SetDestination(transform.position);
		destinationReached = false;
		working = false;
		countDown = false;
		moveTo = false;
		found = false;
		makeRotation = false;
		yRotation = -1;
		turn = false;
		stuck = false;
	}

	private bool Rotation() {
		if (makeRotation && !stuck) {
			if (yRotation == -1) {
				Vector3 direction = (obj.Item2 - transform.position).normalized;
				yRotation = Quaternion.LookRotation(direction).eulerAngles.y;
				Debug.Log(yRotation);
			}
			Vector3 or = transform.eulerAngles;
			Vector3 to = new Vector3(0f, yRotation, 0f);
					
			bool rightTurn;
			if (or.y > to.y) {
				if (Mathf.Abs(or.y - to.y) < Mathf.Abs(or.y - (to.y + 360f)))
					rightTurn = false;
				else {
					rightTurn = true;
					to.y += 360;
				}
			}
			else {
				if (Mathf.Abs(to.y - or.y) < Mathf.Abs(to.y - (or.y + 360f)))
					rightTurn = true;
				else {
					rightTurn = false;
					or.y += 360;
				}
			}

			if (!turn) {
				if (rightTurn)	animator.SetTrigger("RightTurn");
				else			animator.SetTrigger("LeftTurn");
				turn = true;
			}

			bool rotationReached = Vector3.Distance(or, to) <= 1f;
			if (!rotationReached)
				transform.eulerAngles = Vector3.Lerp(or, to, rotationSpeed*Time.deltaTime);
			else {
				transform.eulerAngles = to;
				turn = false;
				makeRotation = false;
				yRotation = -1;
			}
			return rotationReached;
		}
		else 
			return true;
	}

    // Start is called before the first frame update
    void Awake() // Start
    {
		agent=GetComponent<NavMeshAgent>();
		destroyWhenOnDestination=false;
		stuckTimer=0;
		timeToConsiderStuck=0.5f;
		stuckDistance=0.2f;
		randomRadius=30f;
		stuckPosition=transform.position;
		panicking=false;
		idleChance=15;
		if (!idle) {
			alwaysIdle=false;
			timeIdling=UnityEngine.Random.Range(6f,12f);
			idleTimer=0f;
		} else alwaysIdle=true;

		animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		/*if (rotate) { //DEBUG
			Rotation();
		}*/

		if(name == "Female11.1 (1)") { //DEBUG
			if (Input.GetKey("w"))
				transform.position += transform.forward * walkSpeed * Time.deltaTime;
			if (Input.GetKey("s"))
				transform.position -= transform.forward * walkSpeed * Time.deltaTime;
			if (Input.GetKey("a"))
				transform.eulerAngles -= new Vector3(0.0f, 10 * rotSpeed * Time.deltaTime, 0.0f);
			if (Input.GetKey("d"))
				transform.eulerAngles += new Vector3(0.0f, 10 * rotSpeed * Time.deltaTime, 0.0f);
		}

		if (idle && !alwaysIdle) {
			idleTimer+=Time.deltaTime;
			if (idleTimer>=timeIdling) {
				idleTimer=0f;
				idle=false;
			}
		}else if (!idle && !agent.pathPending) {
			if (destroyWhenOnDestination&&agent.remainingDistance<=0.4) Destroy(gameObject);
			else if (agent.remainingDistance<=0.2) {
				if (agenda) { // NEW
					if (moveTo && agent.remainingDistance<=0) {
						if (Rotation()) {
							if (stuck)
								agent.SetDestination(obj.Item1);
							else 
								destinationReached = true;
						}
					}
					else if (!moveTo) {
						SetRandomDestination();
					}
				}
				else {
					if (UnityEngine.Random.Range(0, 100)<idleChance) {
						idle=true;
					}
					else SetRandomDestination();
				}
			} else if (!panicking) CheckIfStuck();
		}
    }
}
