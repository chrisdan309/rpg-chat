using UnityEngine;

public class Movement : MonoBehaviour
{
	public float moveSpeed = 5f;
	private bool isMoving;
	private Vector3 targetPosition;
	private Vector3 input;
	private Vector3 facingDirection = Vector3.up;
	public float interactionRange = 1f;
	public bool isInteracting = false;
	private void Start()
	{
		targetPosition = transform.position;
	}

	void Update()
	{
		if (DialogManager.Instance.IsDialogueActive)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				DialogManager.Instance.HideDialogue();
			}
			return;
		}
		
		if (DialogManager.Instance.IsOptionActive)
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				DialogManager.Instance.HideOptions();
			}
			return;
		}
		
		if (DialogManager.Instance.IsModelActive)
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				DialogManager.Instance.HideModels();
			}
			return;
		}

		if (isMoving)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

			if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
			{
				transform.position = targetPosition;
				isMoving = false;
			}

			return;
		}

		input = Vector3.zero;
		if (Input.GetKey(KeyCode.W)) input = Vector3.up;
		if (Input.GetKey(KeyCode.S)) input = Vector3.down;
		if (Input.GetKey(KeyCode.A)) input = Vector3.left;
		if (Input.GetKey(KeyCode.D)) input = Vector3.right;

		if (input != Vector3.zero)
		{
			facingDirection = input;
			Vector3 newPosition = transform.position + input;

			if (!Physics2D.OverlapCircle(newPosition, 0.1f))
			{
				targetPosition = newPosition;
				isMoving = true;
			}
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			Interact();
		}
	}


	void Interact()
	{
		Debug.Log("Hola");
		RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, interactionRange, LayerMask.GetMask("Interactable"));
		
		if (hit.collider != null)
		{
			IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
			if (interactable != null)
			{
				interactable.Interact();
			}
		}
	}
}