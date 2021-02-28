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
	public bool moveTo = false;
	public bool found = false;
	public float rotationSpeed = 1.5f;
	public bool makeRotation = false;
	public float yRotation = -1;
	public Tuple<GameObject, Vector3, Vector3, string> obj;
	public int queuePos = 0;
	private bool turn = false;
	private bool stuck = false;
	private Animator animator;

	public bool moveAvatar = false;
	public int moveAvatarDirection;
	public Vector3 moveAvatarDest;
	public float moveAvatarSpeed;
	public float moveAvatarDistance = -1f;

	public float walkSpeed; // DEBUG
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
				if (agenda && moveTo) stuck = true;
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

	public void ObjectFound(GameObject objectFound, Vector3 dest, int qp, float rot) {
		obj = new Tuple<GameObject, Vector3, Vector3, string>(objectFound, dest, objectFound.transform.position, objectFound.name);
		yRotation = rot;
		if (queuePos > qp) {
			animator.ResetTrigger("Idle");
			animator.SetTrigger("Walk");
		}
		queuePos = qp;
		found = true;
	}

	public void SetDestinationToObject() {
		agent.SetDestination(obj.Item2);
		moveTo = true;
		if (queuePos == 0) makeRotation = true;
	}

	public void SetFreeObject() {
		IndividualObjectManager iom = obj.Item1.GetComponent<IndividualObjectManager>();
		if (iom) iom.SetFree();
	}

	public void Reset() {
		//agent.SetDestination(transform.position);
		destinationReached = false;
		countDown = false;
		moveTo = false;
		makeRotation = false;
		turn = false;
		stuck = false;
		found = false;
	}

	private bool Rotation() {
		if (makeRotation && !stuck) {
			if (yRotation == -1) {
				Vector3 direction = (obj.Item3 - transform.position).normalized;
				yRotation = Quaternion.LookRotation(direction).eulerAngles.y;
			}
			else if (yRotation == -2) return true;

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

			if (Vector3.Distance(or, to) > 10f) {
				transform.eulerAngles = Vector3.Lerp(or, to, rotationSpeed*Time.deltaTime);
				if (!turn) {
					if (rightTurn) animator.SetTrigger("RightTurn");
					else animator.SetTrigger("LeftTurn");
					turn = true;
				}
				return false;
			}
			else {
				transform.eulerAngles = to;
				turn = false;
				makeRotation = false;
				yRotation = -1;
				return true;
			}
		}
		return true;
	}

	private void MoveAvatar() {
		if (moveAvatar) {
			float remainingDistance = Vector3.Distance(transform.position, moveAvatarDest);
			if (moveAvatarDistance == -1f || remainingDistance < moveAvatarDistance) 
				moveAvatar = remainingDistance >= 0.01;
			else moveAvatar = false;

			if (moveAvatar) {
				transform.position = transform.position + transform.forward * moveAvatarDirection * moveAvatarSpeed*Time.deltaTime;
				//transform.position = Vector3.Lerp(transform.position, moveAvatarDest, moveAvatarSpeed*Time.deltaTime);
				moveAvatarDistance = remainingDistance;
			}
			else {
				transform.position = moveAvatarDest;
				moveAvatarDistance = -1f;
			}
		}
	}

    // Start is called before the first frame update
    void Awake() // Start
    {
		agent=GetComponent<NavMeshAgent>();
		destroyWhenOnDestination=false;
		stuckTimer=0;
		timeToConsiderStuck=0.5f;//0.5f
		stuckDistance=0.2f;//0.2f
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
    void LateUpdate()
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

		MoveAvatar();

		if (idle && !alwaysIdle) {
			idleTimer+=Time.deltaTime;
			if (idleTimer>=timeIdling) {
				idleTimer=0f;
				idle=false;
			}
		}else if (!idle && !agent.pathPending) {
			if (destroyWhenOnDestination&&agent.remainingDistance<=0.4) Destroy(gameObject);
			else if (agent.enabled && agent.remainingDistance<=0.2) {
				if (agenda) { // NEW
					if (moveTo) {
						if (Rotation()) {
							if (stuck) {
								stuck = false;
								agent.SetDestination(obj.Item2);
							}
							else if (queuePos == 0) {
								destinationReached = true;
								idle = true;
							}
							else {
								animator.ResetTrigger("Walk");
								animator.SetTrigger("Idle");
							}
						}
					}
					else SetRandomDestination();
				}
				else {
					if (UnityEngine.Random.Range(0, 100)<idleChance) idle=true;
					else SetRandomDestination();
				}
			}
			else if (agent.enabled && !panicking) CheckIfStuck();
		}
    }
}
