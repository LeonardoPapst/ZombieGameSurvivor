using UnityEngine;
using UnityEngine.UI;

public class ControlZombie : MonoBehaviour
{
    public GameObject Player;
    public float Speed = 5;

    private Rigidbody zombieRigibody;
    private Animator zombieAnimator;
    private ControlPlayer PlayerControl;     

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerControl = Player.GetComponent<ControlPlayer>();

        int randomZombieType = Random.Range(1, 28);
        transform.GetChild(randomZombieType).gameObject.SetActive(true);

        zombieRigibody = GetComponent<Rigidbody>();
        zombieAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        Vector3 direction = Player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        zombieRigibody.MoveRotation(rotation);

        if (distance > 2.5)
        {
            zombieRigibody.MovePosition(
                zombieRigibody.position +
                direction.normalized * Speed * Time.deltaTime
                );
            zombieAnimator.SetBool("Atacando", false);
        }
        else
        {
            zombieAnimator.SetBool("Atacando", true);
        }
    }

    void AtacaJogador()
    {
        if (PlayerControl.NumberOfLives == 0)
        {
            Time.timeScale = 0;
            PlayerControl.GameOverText.SetActive(true);
            PlayerControl.Vivo = false;
        }
        else
        {
            PlayerControl.NumberOfLives -= 1;
        }
        
    }
}
