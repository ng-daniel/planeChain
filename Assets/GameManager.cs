using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;
    public static GameManager Instance{
        get{
            if(_instance is null){
                Debug.LogError("Game Manager is NULL.");
            }
            return _instance;
        }
        
    }

    
    public enum GameState{
        start,
        play,
        pause,
        end,
    }
    public GameState state;


    public float gametime;
    public float gametimer;
    
    public delegate void StartAction();
    public static event StartAction OnStart;
    bool gameStart;
    
    public delegate void EndAction(bool didWin);
    public static event EndAction OnEnd;

    float score;

    public float setScore(float num){
        score = num;
        return score;
    }
    public float addScore(float num){
        score += num;
        return score;
    }
    public float surfaceHP;
    public float surfaceMAXHP;
    public GameObject hurtAlert;
    public float damageSurface(float num){
        surfaceHP -= num;
        if(surfaceHP <= 0 && state != GameState.end){
            _loseGame("surface dead");
        }
        if(surfaceHP<=50 && criticalHUD.activeSelf == false){
            criticalHUD.SetActive(true);
            Instantiate(hurtAlert, transform.position, transform.rotation);
        }
        return surfaceHP;
    }
    public GameObject criticalHUD;
    string lossPurpose;
    public string getLossPurpose(){
        return lossPurpose;
    }

    bool restartPress = false;
    public GameObject hapticSource;

    void Awake(){
        _instance = this;
    }

    public GameObject JukeBox;
    public GameObject winChime;
    public GameObject metalPipe;




    void Start()
    {
        gameStart = false;
        state = GameState.start;
        surfaceHP = surfaceMAXHP;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            if(gameStart == false){
                _gameStartEventCaller();
                gameStart = true;
                Instantiate(hapticSource, transform.position, transform.rotation);
            }
            else if(restartPress==true){
                Instantiate(hapticSource, transform.position, transform.rotation);
                SceneManagerScript.Instance.resetGame();
            }
            
        }
        if(state == GameState.play){
            timerFunction();
        }
    }
    void _gameStartEventCaller(){
        JukeBox.SetActive(true);
        if(OnStart != null){
            OnStart();
            state = GameState.play;
        }
    }
    public void _loseGame(string purpose){
        
        JukeBox.SetActive(false);
        Instantiate(metalPipe, transform.position, transform.rotation);
        lossPurpose = purpose;
        _gameEndEventCaller(false);
    }
    void _gameEndEventCaller(bool didWin){
        endgameScoreCalc(didWin);
        state = GameState.end;
        StartCoroutine(waitToPressSpaceCo());
        
        if(OnEnd != null){
            OnEnd(didWin);
        }
    }
    void timerTick(){
        gametimer+=Time.deltaTime;
    }
    void timerFunction(){
        if(gametimer<gametime){
            timerTick();
        }
        else{
            
        }
    }
    void endgameScoreCalc(bool didWin){
        float temp = score;
        if(didWin){
            temp = temp * 2f;
        }
        score = temp;

    }
    public string getTimeString(){
        int time = (int)Mathf.Ceil(gametime - gametimer);
        if(time<0){
            time = 0;
        }
        int minutes = time/60;
        int seconds = time%60;

        string secondSTR = seconds.ToString();
        if(seconds < 10){
            secondSTR = "0"+secondSTR;
        }
        string timeStr = minutes.ToString() + ":" + secondSTR;
        return timeStr;
    }
    public string getSurfaceString(){
        int surface = (int)Mathf.Ceil(surfaceHP);
        return surface.ToString() + "%";
    }
    public string getScoreString(){
        int scoreInt = (int)Mathf.Ceil(score);
        string scoreString = scoreInt.ToString();
        string temp = "";
        for(int i = scoreString.Length; i<5; i++){
            temp+="0";
        }
        if(scoreString.Length >= 5){
            return "MAX";
        }
        return temp+scoreString;
    }
    public void _winGame(){
        JukeBox.SetActive(false);
        Instantiate(winChime, transform.position, transform.rotation);
        _gameEndEventCaller(true);
    }
    IEnumerator waitToPressSpaceCo(){
        yield return new WaitForSeconds(2f);
        restartPress = true;
    }
    
}
