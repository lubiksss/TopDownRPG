using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    float h; float v;
    bool isHorizonMove;
    public float moveSpeed = 5;
    Vector3 dirVec;
    GameObject scanObject;
    Rigidbody2D rigid;
    Animator anim;
    public GameManager manager;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // move value
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        // Check Button Down & Up
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        // Check Horizontal Move
        if (hDown) { isHorizonMove = true; }
        else if (vDown) { isHorizonMove = false; }
        else if (hUp || vUp) { isHorizonMove = h != 0; }

        // Animation
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else { anim.SetBool("isChange", false); }

        //Direction
        if (vDown && v == 1) { dirVec = Vector3.up; }
        if (vDown && v == -1) { dirVec = Vector3.down; }
        if (hDown && h == -1) { dirVec = Vector3.left; }
        if (hDown && h == 1) { dirVec = Vector3.right; }

        //Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            // Debug.Log("this is: " + scanObject.name);
            manager.Action(scanObject);
        }
    }
    void FixedUpdate()
    {
        // plyaer move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * moveSpeed;

        // Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else { scanObject = null; }
    }
}
