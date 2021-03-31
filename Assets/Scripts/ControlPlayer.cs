using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ControlPlayer : MonoBehaviour
{
    public float NumberOfLives = 3;
    public float Velocidade = 10;
    public LayerMask FloorMask;
    public GameObject GameOverText;
    public Text LivesText;
    public bool Vivo = true;
    public bool Victory = false;
    
    private Vector3 direcao;
    private Rigidbody playerRigibody;
    private Animator playerAnimator;

    void Start()
    {
        Time.timeScale = 1;
        playerRigibody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }
    
    void Update()
    {
        LivesText.text = "Vidas: " + NumberOfLives.ToString();

        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        playerRigibody.MovePosition(
             playerRigibody.position + (direcao * Velocidade * Time.deltaTime)
            );

        if (direcao != Vector3.zero)
        {
            playerAnimator.SetBool("Movendo", true);
        }
        else
        {
            playerAnimator.SetBool("Movendo", false);
        }

        if (!Vivo)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("Hotel");
            }
        }
        if (Victory)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("Hotel");
            }
        }
    }

    void FixedUpdate()
    {
        playerRigibody.MovePosition(
             playerRigibody.position + (direcao * Velocidade * Time.deltaTime)
            );

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        RaycastHit impact;
        if (Physics.Raycast(ray, out impact, 100, FloorMask))
        {
            Vector3 aimPosition = impact.point - transform.position;
            aimPosition.y = transform.position.y;

            Quaternion newRotation = Quaternion.LookRotation(aimPosition);
            playerRigibody.MoveRotation(newRotation);
        }       
    }
}
