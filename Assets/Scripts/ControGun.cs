using UnityEngine;

public class ControGun : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Bullet;
    public GameObject GunBarrel;
   
    private Animator playerAnimator;
    
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnimator.GetBool("Movendo") == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(Bullet, GunBarrel.transform.position, transform.rotation);
            }
        }
    }
}
