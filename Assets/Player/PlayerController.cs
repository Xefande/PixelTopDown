using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 1.5f;
    public float slower = 1f;
    public Rigidbody2D rb;
    public Animator anim; //anim�ci�khoz
    float isRunning = 1f;
    public float maxStamina = 100f;
    public float currentStamina = 50f;
    public float minStaminaToRun = 5f;
    public float staminaLossIfRun = 1f;
    public float staminaRegenRest = 2f;
    public float staminaRegenWalk = 1f;
    public float isRestDelay = 2f;
    public int staminaRegeIfSleep = 40;
    public int maxHP = 100;
    public int currentHP = 50;
    public int minHPtoStaminaRegen = 20;
    public int hpRegen = 0;
    public int dungeonPoints = 0;
    public int keys = 0;
    public int reinforcedKeys = 0;
    public int goldenKeys = 0;

    

    Vector2 movement;

    bool isSleeping = false; // Sleep
    bool canSleep = true; // Player can sleep or not

    bool isWASD = false; // W,A,S,D keys down
    bool isRest = false; // Player rest or not
    bool inVehicle = false; // If player is in vehicle

    private Vector2 lastMoveDirection; // utols� elmozdul�s ir�nya



    // Lelass�tja a j�t�kost �s megsz�nteti az ir�ny�t�st (jelenetv�lt�shoz)

    private bool isFrozen = false;

    public void SetFrozen (bool freeze) // Ha true-t kap, akkor true lesz az isFrozen �rt�ke, ha falseo-t akkor meg False
    {
        isFrozen = freeze;
    }    
    
    public void SlowDown (float slow) // Ha true-t kap, akkor true lesz az isFrozen �rt�ke, ha false-t akkor meg False
    {
        slower = slow;
    }



    // Update is called once per frame
    void Update()
    {
        Inputs();
        Animate();
        CheckWASDKeys();
        HandleStamina();
    }
    void FixedUpdate()
    {
        Movin();

    }
    void Inputs()
    {
        if (isFrozen)
        {
            return;
        }

        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // utols� mozg�s ir�nya (idle anim�ci�hoz)

        if ((moveX == 0 && moveY == 0) && movement.x != 0 || movement.y != 0) //  a j� kurva anyj�t ezzel mennyit szoptam
        {
            lastMoveDirection = movement;
        }

        movement = new Vector2 (moveX, moveY).normalized;


        // fut�s vez�rl�s 
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && currentStamina > minStaminaToRun)
        {
            isRunning = runSpeed;
        }
        else
        {
            isRunning = 1;
        }

        //alv�s vez�r�s

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Y) && canSleep)
        {
            Sleep();
        }






    }
    void HandleStamina()
    {
        if (isRunning > 1.0f)
        {
            currentStamina = currentStamina - (staminaLossIfRun * Time.deltaTime);
        }

        // Stamina Regener�ci�

        // Ha alszik, ha egy helyben �ll azaz pihen ha pihen, ha s�t�l, ha hp-ja t�bb mint 20

        if (isSleeping)
        {
            // GoSleep Anim
            
            
            // Fade out


            // Stamina Regen
            if (maxStamina > currentStamina + staminaRegeIfSleep)
            {
                currentStamina = currentStamina + staminaRegeIfSleep;
            }
            else
            {
                currentStamina = maxStamina;
            }


            // HP regen


            // Fade in


            // Wake Up Anim


            // Sleep Over
            isSleeping = false;
            canSleep = false;

        }

        // Pihen�skor
        /*
        isRestOrNot();
        if (isRest && currentStamina < maxStamina && currentHP > minHPtoStaminaRegen) // Egy helyben van vagy j�rm�ben van �s van el�g HP-ja
        {
            currentStamina = currentStamina + (staminaRegenRest * Time.deltaTime);
            isRest = false;
        }*/


        StartCoroutine(isRestOrNot()); // K�sleltetve kezdi el felt�lteni a HP-t
        if (isRest && currentStamina < maxStamina && currentHP > minHPtoStaminaRegen) // Egy helyben van vagy j�rm�ben van �s van el�g HP-ja
        {
            currentStamina = currentStamina + (staminaRegenRest * Time.deltaTime);
            isRest = false;
        }



        //S�t�l�skor
        if (isRest == false && isRunning <= 1.0f && currentStamina < maxStamina && currentHP > minHPtoStaminaRegen) // Nem pihen, nem fut, �s a stamina kevesebb mint a maxim�lis stamina �s van el�g HP-ja
        {
            currentStamina = currentStamina + (staminaRegenWalk * Time.deltaTime);
            isRest = false;
        }

    }


    void Sleep()
    {
        isSleeping = true;
        Debug.Log("Sleep");
    }


    void Movin()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * slower * isRunning * Time.fixedDeltaTime);
    }

 /*   void isRestOrNot() // Pihen-e
    {
        isWASD = CheckWASDKeys();
        if (inVehicle == false && isWASD == true) // Ha j�rm�ben van vagy nem s�t�l, akkor pihen
        {
            isRest = false;
        }
        else 
        {
            yield return new WaitForSeconds(2.0f);
            isRest = true;
        }
    }*/

    IEnumerator isRestOrNot() // Pihen-e
    {
        isWASD = CheckWASDKeys();
        if (inVehicle == false && isWASD == true) // Ha j�rm�ben van vagy NEM s�t�l, akkor pihen
        {
            isRest = false;
        }
        else
        {
            yield return new WaitForSeconds(isRestDelay);
            isRest = true;
        }
    }


    void Animate()
    {
        anim.SetFloat("AnimMoveX", movement.x);
        anim.SetFloat("AnimMoveY", movement.y);
        anim.SetFloat("AnimMoveMagnitude", movement.magnitude);
        anim.SetFloat("AnimLastMoveX", lastMoveDirection.x);
        anim.SetFloat("AnimLastMoveY", lastMoveDirection.y);

    }



    // Nyomva van-e WASD billenty�
    private bool CheckWASDKeys()
    {
        bool isW, isA, isS, isD;
        bool isWASD = false;

        if (Input.GetKey(KeyCode.W))
        {
            isW = true;
        }
        else 
        {
            isW = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            isA = true;
        }
        else
        {
            isA = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            isS = true;
        }
        else
        {
            isS = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            isD = true;
        }
        else
        {
            isD = false;
        }

        if (isW || isA || isS || isD)
        {
            isWASD = true;
        }
        else
        {
            isWASD = false;
        }
        return isWASD;
    }

}
