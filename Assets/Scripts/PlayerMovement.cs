using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;

public class PlayerMove : MonoBehaviour
{
    private float movementX, movementY;
    public float moveForce = 5;
    public GameObject question, buttonA, buttonB, buttonC, enemy1, enemy2, enemy3;
    public Text text, resultText, studyAgainText, oneText, threeText;
    private FlashCardManager flashCardManager;
    private Text buttonAText, buttonBText, buttonCText;
    private int questionIndex = 0;
    private int numRights = 0;
    private bool isAnswering = false;
    private SpriteRenderer sr;
    private Animator animator;

    /**
        Awake is called when the script instance is being loaded.
        Awake is called either when an active GameObject that contains the script is initialized when a Scene loads, 
        or when a previously inactive GameObject is set to active, or after a GameObject created with Object.
        Instantiate is initialized. Use Awake to initialize variables or states before the application starts.
    */
    void Awake() {
        flashCardManager = new FlashCardManager();
        buttonAText = buttonA.GetComponentInChildren<Text>();
        buttonBText = buttonB.GetComponentInChildren<Text>();
        buttonCText = buttonC.GetComponentInChildren<Text>();

        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonA.GetComponent<Button>().onClick.AddListener(ButtonAClick); 
        buttonB.GetComponent<Button>().onClick.AddListener(ButtonBClick);  
        buttonC.GetComponent<Button>().onClick.AddListener(ButtonCClick); 
        resultText.text = "" + numRights + "/" + flashCardManager.questions.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAnswering){
            movementX = Input.GetAxisRaw("Horizontal");
            transform.position += new Vector3(movementX, 0f, 0f) * moveForce * Time.deltaTime;

            movementY = Input.GetAxisRaw("Vertical");
            transform.position += new Vector3(0f, movementY, 0f) * moveForce * Time.deltaTime;

            if (movementX > 0) {
                sr.flipX = false;
                animator.SetBool("run", true);
            } else if (movementX < 0){
                sr.flipX = true;
                animator.SetBool("run",true);
            } else {
                animator.SetBool("run",false);

            }
        }
    }

    private void SetQuestionActive(bool isActive) {
        question.SetActive(isActive); 
        text.gameObject.SetActive(isActive);
        buttonA.SetActive(isActive);
        buttonB.SetActive(isActive);
        buttonC.SetActive(isActive);

        isAnswering = isActive;
    }

    private void SetQuestion(int index) {
        text.text = flashCardManager.questions[index].question;
        buttonAText.text = flashCardManager.questions[index].answerA;
        buttonBText.text = flashCardManager.questions[index].answerB;
        buttonCText.text = flashCardManager.questions[index].answerC;
        questionIndex = index;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Question1"))  {
            SetQuestionActive(true);
            SetQuestion(0);
        }

        if (collision.CompareTag("Question2"))  {
            SetQuestionActive(true);
            SetQuestion(1);
        }
        if (collision.CompareTag("Question3"))  {
            SetQuestionActive(true);
            SetQuestion(2);
        }

        if (collision.CompareTag("Destination"))  {
            Debug.Log("eqweqwe");
            if (numRights == flashCardManager.questions.Length){
                StartCoroutine(ActivationRoutineExit(threeText));
            } else {
                StartCoroutine(ActivationRoutineExit(oneText));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Question1"))  {
            SetQuestionActive(false);
            SetQuestion(0);
        }

        if (collision.CompareTag("Question2"))  {
            SetQuestionActive(false);
            SetQuestion(1);
        }
        if (collision.CompareTag("Question3"))  {
            SetQuestionActive(false);
            SetQuestion(2);
        }
    }


    void DestroyEnemy() {
        if (questionIndex == 0){
            Destroy(enemy1);
        }
        if (questionIndex == 1) {
            Destroy(enemy2);
        }
        if (questionIndex == 2) {
            Destroy(enemy3);
        }
    }
    void ButtonAClick(){
        if (flashCardManager.questions[questionIndex].indexOfResult == 0){
            DestroyEnemy();
            increaseNumRights();
        } else {
            SetQuestionActive(false);
            StartCoroutine(ActivationRoutine(studyAgainText));
        }
    }

    void ButtonBClick(){
        if (flashCardManager.questions[questionIndex].indexOfResult == 1){
            DestroyEnemy();
            increaseNumRights();
        }
        else {
            SetQuestionActive(false);
            StartCoroutine(ActivationRoutine(studyAgainText));
        }
    }

    void ButtonCClick(){
        if (flashCardManager.questions[questionIndex].indexOfResult == 2){
            DestroyEnemy();
            increaseNumRights();
        } else {
            SetQuestionActive(false);
            StartCoroutine(ActivationRoutine(studyAgainText));
        }
    }

    void increaseNumRights() {
        numRights ++;
        resultText.text = "" + numRights + "/" + flashCardManager.questions.Length;
    }

    // void DisplayStudyAgain() {
    //     studyAgainText.gameObject.SetActive(true);
    //     Task.Delay(new TimeSpan(3000)).ContinueWith(o => {
    //         HideStudyAgain();
    //     });
    // }

    // void HideStudyAgain(){
    //     studyAgainText.gameObject.SetActive(false);
    //     Debug.Log("abc");
    // }

    private IEnumerator ActivationRoutine(Text text){
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        text.gameObject.SetActive(false);
    }

    private IEnumerator ActivationRoutineExit(Text text){
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        text.gameObject.SetActive(false);
        Application.Quit();
    }
}


