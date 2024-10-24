using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class MenuScript : MonoBehaviour
{

    public GameObject Controls;
    public GameObject StartScreen;
    public GameObject PauseScreen;
    public GameObject EndScreen;
    public GameObject BalanceBar;
    private GameObject craneBottom;
    private HingeJoint hinge;
    [SerializeField] private GameObject[] levelInd;

    [SerializeField] private Camera cam;
    private float menuClipping = 26f;
    private float gameClipping = 37f;

    public GameObject Story1;
    public GameObject Story2;
    public GameObject Story3;
    public GameObject Story4;
    public GameObject Story5;

    private SpawnControlls controls;

    public GameObject screenshotCamera;
    //1 = StartScreen, 2= InGame, 3=EndScreen, 4=PauseScreen, 5=Storytime, 6=Settings, 7=ControlsScreen
    private int ScreenState = 1;
    //number represents which slide
    private int StoryState = 0;
    // Start is called before the first frame update


    private AudioSource playMusic;
    public AudioClip background1;
    public AudioClip background2;
    public GameObject Vslider;
    private GameObject sounds;

    public GameObject settingscreen;
    public GameObject controlsscreen;
    public GameObject nosettings;

    private void Awake()
    {
        controls = new SpawnControlls();
        //controlls.Enable();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    void Start()
    {
        cam.nearClipPlane = menuClipping;
        StartScreen.SetActive(true);
        Controls.SetActive(false);
        BalanceBar.SetActive(false);
        playMusic = GetComponent<AudioSource>();
        playMusic.clip = background1;
        playMusic.Play();

        /*
        StartScreen.SetActive(false);
        Controls.SetActive(true);
        BalanceBar.SetActive(true);
        screenshotCamera.SetActive(false);
        Globals.isGameStart = true;
        ScreenState = 2;*/

        craneBottom = GameObject.FindWithTag("CraneBottom");
        sounds = GameObject.FindWithTag("soundsglobal");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Start game button
        if(controls.Player.StartGame.triggered){
            cam.nearClipPlane = gameClipping;
            if (ScreenState == 1){
                sounds.GetComponent<SoundsGlobal>().playSound("menuConfirm");
                StartScreen.SetActive(false);
                Controls.SetActive(true);
                BalanceBar.SetActive(true);
                screenshotCamera.SetActive(false);
                Globals.isGameOver = false;
                Globals.isGameStart = true; 
                ScreenState = 2;
                levelInd[0].GetComponent<LT2>().goin();
                Globals.BlockTypeChoice = 1;
                playMusic.Stop();
                playMusic.clip = background2;
                playMusic.Play();
            }else if(ScreenState == 3){
                sounds.GetComponent<SoundsGlobal>().playSound("menuConfirm");
                Controls.SetActive(true);
                BalanceBar.SetActive(true);
                EndScreen.SetActive(false);
                screenshotCamera.SetActive(false);
                Globals.isGameOver = false;
                Globals.isGameStart = true;
                ScreenState = 2;
                Globals.isGameStory = false;
                levelInd[0].GetComponent<LT2>().goin();
                Globals.BlockTypeChoice = 1;
                ClearScene();
                startGame();  
                playMusic.Stop();
                playMusic.clip = background2;
                playMusic.Play();
            }
        }
        //logic to end the game and show gameover
        if(Globals.isGameOver && ScreenState == 2){
            sounds.GetComponent<SoundsGlobal>().playSound("defeat");
            cam.nearClipPlane = menuClipping;
            EndScreen.SetActive(true);
            screenshotCamera.SetActive(true);
            Controls.SetActive(false);
            BalanceBar.SetActive(false);
            ScreenState = 3;
            Globals.setDefault();
            playMusic.Stop();
            playMusic.clip = background1;
            playMusic.Play();

        }

        //button to return to the start menu
        if(controls.Player.GoStartScreen.triggered){
            cam.nearClipPlane = menuClipping;
            if (ScreenState == 3){
                sounds.GetComponent<SoundsGlobal>().playSound("menuCancel");
                EndScreen.SetActive(false);
                screenshotCamera.SetActive(false);
                StartScreen.SetActive(true);
                ScreenState = 1;
                playMusic.Stop();
                playMusic.clip = background1;
                playMusic.Play();
            }else if(ScreenState == 4){
                sounds.GetComponent<SoundsGlobal>().playSound("menuCancel");
                PauseScreen.SetActive(false);
                StartScreen.SetActive(true);
                BalanceBar.SetActive(false);
                Globals.isGameStart = false; 
                Globals.isGamePause = false;
                Globals.setDefault();
                ScreenState = 1;
                playMusic.Stop();
                playMusic.clip = background1;
                playMusic.Play();
            }else if(ScreenState == 6){
                sounds.GetComponent<SoundsGlobal>().playSound("menuCancel");
                print("test");
                Application.Quit();
            }
            Globals.isGameStory = false;
            Globals.BlockTypeChoice = 3;
            ClearScene();
            startGame();
        }

        //button to end the game completely
        if(controls.Player.EndGame.triggered){
            if(ScreenState==1){
                sounds.GetComponent<SoundsGlobal>().playSound("menuCancel");
                print("test");
                Application.Quit();
            }else if(ScreenState == 2){
                sounds.GetComponent<SoundsGlobal>().playSound("menuCancel");
                Globals.isGamePause = true;
                Controls.SetActive(false);
                PauseScreen.SetActive(true);
                ScreenState = 4;
            }else if(ScreenState == 4){
                sounds.GetComponent<SoundsGlobal>().playSound("menuCancel");
                Globals.isGamePause = false;
                Controls.SetActive(true);
                PauseScreen.SetActive(false);
                ScreenState = 2;
            }else if(ScreenState == 6){
                sounds.GetComponent<SoundsGlobal>().playSound("menuCancel");
                settingscreen.SetActive(false);
                nosettings.SetActive(false);
                ScreenState = 1;
            }
            else if(ScreenState == 7){
                sounds.GetComponent<SoundsGlobal>().playSound("menuCancel");
                controlsscreen.SetActive(false);
                ScreenState = 1;
            }
        }

        //button to go to the story mode
        if(controls.Player.StartStory.triggered){
            cam.nearClipPlane = menuClipping;
            if (ScreenState==1){
                sounds.GetComponent<SoundsGlobal>().playSound("menuConfirm");
                print("loadingscene");
                //load one scene
                StartScreen.SetActive(false);
                Story1.SetActive(true);
                ScreenState = 5;
                StoryState = 1;
                Globals.isGameStory = true;
                Globals.BlockTypeChoice = 1;
                playMusic.Stop();
                playMusic.clip = background2;
                playMusic.Play();
                sounds.GetComponent<SoundsGlobal>().playSound("transistion");
            }
        }

        if(controls.Player.MoveForward.triggered){
            if(ScreenState==5){
                if(StoryState ==1){
                    sounds.GetComponent<SoundsGlobal>().playSound("transistion");
                    Story1.SetActive(false);
                    Story2.SetActive(true);
                    StoryState = 2;
                }else if(StoryState ==2){
                    sounds.GetComponent<SoundsGlobal>().playSound("transistion");
                    Story2.SetActive(false);
                    Story3.SetActive(true);
                    StoryState = 3;
                } else if(StoryState ==3){
                    sounds.GetComponent<SoundsGlobal>().playSound("transistion");
                    Story3.SetActive(false);
                    Story4.SetActive(true);
                    StoryState = 4;
                }else if(StoryState ==4){
                    sounds.GetComponent<SoundsGlobal>().playSound("transistion");
                    Story4.SetActive(false);
                    Story5.SetActive(true);
                    StoryState = 5;
                }
                else if(StoryState == 5){
                    sounds.GetComponent<SoundsGlobal>().playSound("menuConfirm");
                    Story5.SetActive(false);
                    Controls.SetActive(true);
                    BalanceBar.SetActive(true);
                    screenshotCamera.SetActive(false);
                    Globals.startStory();
                    Globals.isGameStart = true; 
                    ScreenState = 2;
                    StoryState =0;
                    levelInd[0].GetComponent<LT2>().goin();
                }

            }

        }

        //button to go to the settings page
        if(controls.Player.Settings.triggered){
            if(ScreenState==1){
                sounds.GetComponent<SoundsGlobal>().playSound("menuConfirm");
                settingscreen.SetActive(true);
                nosettings.SetActive(true);
                ScreenState = 6;
            }
        }

        //button to go to the gallery page
        if(controls.Player.GoControlsScreen.triggered){
            if(ScreenState==1){
                sounds.GetComponent<SoundsGlobal>().playSound("menuConfirm");
                controlsscreen.SetActive(true);
                ScreenState = 7;
            }
        }

        
    }


    void ClearScene(){
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        GameObject[] blocksM = GameObject.FindGameObjectsWithTag("BlockM");
        GameObject[] screens = GameObject.FindGameObjectsWithTag("Level");
        GameObject[] screensM = GameObject.FindGameObjectsWithTag("LevelM");
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        GameObject[] bills = GameObject.FindGameObjectsWithTag("Bill");
        GameObject[] stocks = GameObject.FindGameObjectsWithTag("Stock");
        GameObject[] discounts = GameObject.FindGameObjectsWithTag("Discount");
        GameObject[] lawsuits = GameObject.FindGameObjectsWithTag("Lawsuit");


        foreach (GameObject blockM in blocksM)
        {
            Destroy(blockM);
        }
        foreach (GameObject levelM in screensM)
        {
            Destroy(levelM);
        }
        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
        foreach (GameObject bill in bills)
        {
            Destroy(bill);
        }
        foreach (GameObject stock in stocks)
        {
            Destroy(stock);
        }
        foreach (GameObject discount in discounts)
        {
            Destroy(discount);
        }
        foreach (GameObject lawsuit in lawsuits)
        {
            Destroy(lawsuit);
        }
        foreach(GameObject ind in levelInd){
            ind.GetComponent<LT2>().goback();
        }
        RemoveCraneItem();
        Globals.BlockMovement = 1;
        Globals.BlockTypeChoice = 1;
        Globals.direction = new Vector3(0, -1, 0);
        Globals.BlockMovementPlus = 0;
        Globals.ODX = 0f;
        Globals.ODY = 0f;
        Globals.BlockOneDirection = false;
    }

    private void startGame()
    {
        StartNewLevel script = GetComponent<StartNewLevel>();
        script.startNewLevel(true);
    }
    void RemoveCraneItem(){
        hinge = craneBottom.GetComponent<HingeJoint>();
        Destroy(hinge);
    }

    public void setVolume(){
        float volume = Vslider.GetComponent<Slider>().value;
        playMusic.volume = volume;
    }
}
