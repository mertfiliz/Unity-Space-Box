using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public static Movement ins;

    public AudioSource GameSound, SafeZoneSound, BounceSound, TrapSound;

    public List<GameObject> SafeZonesList = new List<GameObject>();
    public List<GameObject> TrapList = new List<GameObject>();
    public GameObject Cam_Checkpoint;
    public GameObject SafeZone, SafeZones, SafeZone_Start;
    public GameObject Trap_1, Trap_2, Traps;
    public GameObject Bounce_Line;
    public GameObject MoveCamera;
    public Color ObsColor_Green;

    public Sprite[] Planet_Images;

    Vector3 startPos, endPos;
    Camera camera;
    LineRenderer lr;

    Vector3 Checkpoint;

    // CALCULATE SCORE
    public int score;
    private float score_combo;
    private float current_distance;
    private float current_try;
    private float perfect_run;
    public Text Score_Text, Score_Combo, Score_PopUp;

    // LEVEL
    private int level;
    public Text Level;

    // MOUSE HELD DOWN
    private bool score_decrease = false;

    // COROUTINE
    private bool isCameraMoveToRight = false;

    // SCREEN SIZE
    private float ScreenSize_X;
    private float ScreenSize_Y;

    // VOLUMES
    private int music_volume, sound_volume;

    // QUALITY
    public GameObject Post_Processing;
    private int quality_selection;

    public float time_speed = 3f;


    Vector3 camOffset = new Vector3(0, 0, 10);
    [SerializeField] AnimationCurve ac;

    void Awake()
    {
        ins = this;
    }
   
    void Start()
    {
        Application.targetFrameRate = 100;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        StartGame();
        GetScreenSizes();
    }

    void Update()
    {
        if(!GameOverController.game_over_menu)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lr = gameObject.GetComponent<LineRenderer>();
                lr.enabled = true;
                lr.positionCount = 2;
                startPos = this.transform.position + camOffset;
                lr.SetPosition(0, startPos);
                lr.useWorldSpace = true;
                lr.widthCurve = ac;
                lr.numCapVertices = 10;
            }
            if (Input.GetMouseButton(0))
            {
                startPos = this.transform.position + camOffset;
                lr.SetPosition(0, startPos);
                endPos = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
                lr.SetPosition(1, endPos);

                if (score_decrease)
                {
                    Time.timeScale = 0.3f;
                    if (level < 10)
                    {
                        if (score >= 10)
                        {
                            score -= 10;
                            Score_Text.text = score.ToString();
                        }
                    }
                    else if (level < 20)
                    {
                        if (score >= 30)
                        {
                            score -= 30;
                            Score_Text.text = score.ToString();
                        }
                    }
                    else if (level < 30)
                    {
                        if (score >= 60)
                        {
                            score -= 60;
                            Score_Text.text = score.ToString();
                        }
                    }
                    else if (level < 40)
                    {
                        if (score >= 70)
                        {
                            score -= 70;
                            Score_Text.text = score.ToString();
                        }
                    }
                    else if (level < 50)
                    {
                        if (score >= 80)
                        {
                            score -= 80;
                            Score_Text.text = score.ToString();
                        }
                    }
                    else if (level < 60)
                    {
                        if (score >= 100)
                        {
                            score -= 100;
                            Score_Text.text = score.ToString();
                        }
                    }
                    else if (level < 70)
                    {
                        if (score >= 120)
                        {
                            score -= 120;
                            Score_Text.text = score.ToString();
                        }
                    }
                    else if (level >= 70)
                    {
                        if (score >= 140)
                        {
                            score -= 140;
                            Score_Text.text = score.ToString();
                        }
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                lr.enabled = false;
                this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                this.GetComponent<Rigidbody2D>().angularVelocity = 0;
                Time.timeScale = time_speed;
                this.GetComponent<Rigidbody2D>().AddForce((startPos - endPos) * 50);
                this.transform.GetChild(1).GetComponent<TrailRenderer>().Clear();
                this.transform.GetChild(1).GetComponent<TrailRenderer>().time = 1;

                current_try++;
            }
        }       
    }

    void StartGame()
    {
        Load_Quality();
        Load_Volumes();
        GameSound.Play();

        // SET INITIAL VALUES
        score = 0;
        Score_Text.text = score.ToString();

        level = 1;
        Level.text = level.ToString();

        ObsColor_Green = GameObject.Find("Left_Obstacle").GetComponent<Image>().color;

        perfect_run = 0;
        score_combo = 1;
        Score_Combo.text = "x" + score_combo.ToString();

        SafeZonesList.RemoveAt(0);
        SafeZonesList.Add(SafeZone_Start);
        camera = Camera.main;
        Checkpoint = SafeZonesList[0].transform.localPosition;
        this.transform.localPosition = Checkpoint;

        this.transform.GetChild(1).GetComponent<TrailRenderer>().Clear();
        this.transform.GetChild(1).GetComponent<TrailRenderer>().time = 0;
    }

    public void Load_Quality()
    {
        PlayerDataQuality loadedDataQuality = SaveLoadQuality.LoadPlayer();
        quality_selection = loadedDataQuality.quality_selection;

        if(quality_selection == 0)
        {
            Post_Processing.SetActive(false);
        }
        else if (quality_selection == 1)
        {
            Post_Processing.SetActive(true);
        }
    }

    public void Load_Volumes()
    {
        PlayerDataVolumes loadedDataVolumes = SaveLoadVolumes.LoadPlayer();
        music_volume = loadedDataVolumes.m_volume;
        sound_volume = loadedDataVolumes.s_volume;

        Set_Volume_Settings();
    }

    public void Set_Volume_Settings()
    {
        if(music_volume == 0)
        {
            GameSound.volume = 0f;
        }
        if (music_volume == 1)
        {
            GameSound.volume = 0.06f;
        }
        if (music_volume == 2)
        {
            GameSound.volume = 0.12f;
        }
        if (music_volume == 3)
        {
            GameSound.volume = 0.18f;
        }
        if (music_volume == 4)
        {
            GameSound.volume = 0.24f;
        }
        if (music_volume == 5)
        {
            GameSound.volume = 0.30f;
        }

        if(sound_volume == 0)
        {
            SafeZoneSound.volume = 0f;
            BounceSound.volume = 0f;
            TrapSound.volume = 0f;
        }
        if (sound_volume == 1)
        {
            SafeZoneSound.volume = 0.2f;
            BounceSound.volume = 0.2f;
            TrapSound.volume = 0.12f;
        }
        if (sound_volume == 2)
        {
            SafeZoneSound.volume = 0.4f;
            BounceSound.volume = 0.4f;
            TrapSound.volume = 0.24f;
        }
        if (sound_volume == 3)
        {
            SafeZoneSound.volume = 0.6f;
            BounceSound.volume = 0.6f;
            TrapSound.volume = 0.36f;
        }
        if (sound_volume == 4)
        {
            SafeZoneSound.volume = 0.8f;
            BounceSound.volume = 0.8f;
            TrapSound.volume = 0.48f;
        }
        if (sound_volume == 5)
        {
            SafeZoneSound.volume = 1f;
            BounceSound.volume = 1f;
            TrapSound.volume = 0.6f;
        }
    }

    public void GetScreenSizes()
    {
        ScreenSize_X = Screen.width;
        ScreenSize_Y = Screen.height;

        float left_obs = GameObject.Find("Left_Obstacle").transform.localPosition.x;
        float right_obs = GameObject.Find("Right_Obstacle").transform.localPosition.x;

        float bottom_obs = GameObject.Find("Bottom_Obstacle").transform.localPosition.y;
        float top_obs = GameObject.Find("Top_Obstacle").transform.localPosition.y;
    }

    void Create_SafeZones()
    {
        var ins = Instantiate(SafeZone, SafeZones.transform.localPosition, Quaternion.identity, SafeZones.transform);

        float min = SafeZonesList[SafeZonesList.Count - 1].transform.localPosition.x + 500;
        float max = min + 1000;
        float rnd_x = UnityEngine.Random.Range(min, max);
        float rnd_y = UnityEngine.Random.Range(-300, 300);

        // GET RANDOM PLANET IMG
        int rnd_planet_img = UnityEngine.Random.Range(0, Planet_Images.Length);

        ins.GetComponent<Image>().sprite = Planet_Images[rnd_planet_img];
        ins.transform.localPosition = new Vector3(rnd_x, rnd_y, 0);

        float scale = 1f;
        if (level < 10)
        {
            scale = UnityEngine.Random.Range(1f, 1.1f);
        }
        else if (level < 20)
        {
            scale = UnityEngine.Random.Range(0.8f, 1f);

        }
        else if (level < 30)
        {
            scale = UnityEngine.Random.Range(0.6f, 0.8f);
        }
        else if (level < 40)
        {
            scale = UnityEngine.Random.Range(0.5f, 0.75f);

            int rnd_move = UnityEngine.Random.Range(0, 2);
            SafeZone_Move(ins);
            Obstacle_Trap(0);
        }
        else if (level < 50)
        {
            scale = UnityEngine.Random.Range(0.5f, 0.75f);
            SafeZone_Move(ins);
            Obstacle_Trap(0);
        }
        else if (level >= 50)
        {
            scale = UnityEngine.Random.Range(0.5f, 0.75f);
            SafeZone_Move(ins);

            Obstacle_Trap(1);

            if (!isCameraMoveToRight)
            {
                StartCoroutine(MoveCameraToRight());
            }
        }

        ins.transform.localScale = new Vector3(scale, scale, 1);

        SafeZonesList.Add(ins);
    }

    IEnumerator MoveCameraToRight()
    {
        isCameraMoveToRight = true;

        var moverightspeed = 0.03f;
        while (true)
        {          
            if(level > 50)
            {
                moverightspeed = 0.025f;
            }
            if (level > 60)
            {
                moverightspeed = 0.02f;
            }
            if (level > 70)
            {
                moverightspeed = 0.015f;
            }
            if (level > 75)
            {
                moverightspeed = 0.010f;
            }
            MoveCamera.transform.localPosition = new Vector3(MoveCamera.transform.localPosition.x + 1, MoveCamera.transform.localPosition.y, 1);
            yield return new WaitForSeconds(moverightspeed);
        }
        yield return null;
    }

    public void SafeZone_Move(GameObject ins)
    {
        int rnd_move = UnityEngine.Random.Range(0, 2);

        if (rnd_move == 0)
        {
            ins.GetComponent<SafeZoneMove>().enableMove = true;
            ins.GetComponent<SafeZoneMove>().enableShrink = false;
        }
        if (rnd_move == 1)
        {
            ins.GetComponent<SafeZoneMove>().enableMove = false;
            ins.GetComponent<SafeZoneMove>().enableShrink = true;
        }
    }

    public void Obstacle_Trap(int ver)
    {
        // VER 0 (4 OBS) - VER 1 (3 OBS - LEFT RED ALWAYS)

        if (ver == 0)
        {
            // RESET OBSTACLE VALUES
            GameObject.Find("Left_Obstacle").GetComponent<Image>().color = ObsColor_Green;
            GameObject.Find("Top_Obstacle").GetComponent<Image>().color = ObsColor_Green;
            GameObject.Find("Right_Obstacle").GetComponent<Image>().color = ObsColor_Green;
            GameObject.Find("Bottom_Obstacle").GetComponent<Image>().color = ObsColor_Green;

            GameObject.Find("Left_Obstacle").tag = "bounce";
            GameObject.Find("Top_Obstacle").tag = "bounce";
            GameObject.Find("Right_Obstacle").tag = "bounce";
            GameObject.Find("Bottom_Obstacle").tag = "bounce";


            // CREATE NEW OBSTACLE VALUES
            int rnd_obs = UnityEngine.Random.Range(0, 4);
            if (rnd_obs == 0)
            {
                GameObject.Find("Left_Obstacle").tag = "obs";
                GameObject.Find("Left_Obstacle").GetComponent<Image>().color = Color.red;
            }
            if (rnd_obs == 1)
            {
                GameObject.Find("Top_Obstacle").tag = "obs";
                GameObject.Find("Top_Obstacle").GetComponent<Image>().color = Color.red;
            }
            if (rnd_obs == 2)
            {
                GameObject.Find("Right_Obstacle").tag = "obs";
                GameObject.Find("Right_Obstacle").GetComponent<Image>().color = Color.red;
            }
            if (rnd_obs == 3)
            {
                GameObject.Find("Bottom_Obstacle").tag = "obs";
                GameObject.Find("Bottom_Obstacle").GetComponent<Image>().color = Color.red;
            }
        }

        if (ver == 1)
        {
            // RESET OBSTACLE VALUES
            GameObject.Find("Left_Obstacle").GetComponent<Image>().color = Color.red;
            GameObject.Find("Top_Obstacle").GetComponent<Image>().color = ObsColor_Green;
            GameObject.Find("Right_Obstacle").GetComponent<Image>().color = ObsColor_Green;
            GameObject.Find("Bottom_Obstacle").GetComponent<Image>().color = ObsColor_Green;

            GameObject.Find("Left_Obstacle").tag = "obs";
            GameObject.Find("Top_Obstacle").tag = "bounce";
            GameObject.Find("Right_Obstacle").tag = "bounce";
            GameObject.Find("Bottom_Obstacle").tag = "bounce";


            // CREATE NEW OBSTACLE VALUES
            int rnd_obs = UnityEngine.Random.Range(0, 3);

            if (rnd_obs == 0)
            {
                GameObject.Find("Top_Obstacle").tag = "obs";
                GameObject.Find("Top_Obstacle").GetComponent<Image>().color = Color.red;
            }
            if (rnd_obs == 1)
            {
                GameObject.Find("Right_Obstacle").tag = "obs";
                GameObject.Find("Right_Obstacle").GetComponent<Image>().color = Color.red;
            }
            if (rnd_obs == 2)
            {
                GameObject.Find("Bottom_Obstacle").tag = "obs";
                GameObject.Find("Bottom_Obstacle").GetComponent<Image>().color = Color.red;
            }
        }
    }

    void Create_Traps()
    {
        float dist = Mathf.Abs(SafeZonesList[SafeZonesList.Count - 1].transform.localPosition.x - SafeZonesList[SafeZonesList.Count - 2].transform.localPosition.x);

        if (dist < 600)
        {
            // NO TRAP
        }

        else if (dist < 1200)
        {
            var ins = Instantiate(Trap_1, Traps.transform.localPosition, Quaternion.identity, Traps.transform);

            float min = SafeZonesList[SafeZonesList.Count - 2].transform.localPosition.x + 300;
            float max = SafeZonesList[SafeZonesList.Count - 1].transform.localPosition.x - 300;
            float rnd_x = UnityEngine.Random.Range(min, max);
            float rnd_y = UnityEngine.Random.Range(-300, 300);

            ins.transform.localPosition = new Vector3(rnd_x, rnd_y, 0);
            TrapList.Add(ins);
        }

        else if (dist < 2400)
        {
            // CREATE MOVING TRAP

            int Random_MoveTrap = 0;
            if (level <= 10)
            {
                Random_MoveTrap = 1;
            }
            else if (level <= 20)
            {
                Random_MoveTrap = UnityEngine.Random.Range(1, 3); // 1,2
            }
            else if (level <= 30)
            {
                Random_MoveTrap = UnityEngine.Random.Range(1, 4); // 1,2,3

            }
            else if (level > 30)
            {
                Random_MoveTrap = UnityEngine.Random.Range(1, 4); // 1,2,3
            }

            float min = SafeZonesList[SafeZonesList.Count - 2].transform.localPosition.x + 500;
            float max = SafeZonesList[SafeZonesList.Count - 1].transform.localPosition.x - 500;
            float rnd_x = UnityEngine.Random.Range(min, max);
            float rnd_y = UnityEngine.Random.Range(-300, 300);

            float distance_btw_x = UnityEngine.Random.Range(100, 161);

            if (rnd_x + (3 * distance_btw_x) <= SafeZonesList[SafeZonesList.Count - 1].transform.localPosition.x - 280)
            {
                for (int i = 0; i < Random_MoveTrap; i++)
                {
                    //Debug.Log("r" + Random_MoveTrap);
                    var ins = Instantiate(Trap_2, Traps.transform.localPosition, Quaternion.identity, Traps.transform);
                    ins.GetComponent<Trap_2>().add_speed = UnityEngine.Random.Range(0.7f, 1.2f);
                    ins.transform.localPosition = new Vector3(rnd_x + (i * distance_btw_x), 400, 0);
                    TrapList.Add(ins);
                }
            }

            else if (rnd_x + (3 * distance_btw_x) > SafeZonesList[SafeZonesList.Count - 1].transform.localPosition.x - 280)
            {
                for (int i = 0; i < Random_MoveTrap; i++)
                {
                    var ins = Instantiate(Trap_2, Traps.transform.localPosition, Quaternion.identity, Traps.transform);
                    ins.GetComponent<Trap_2>().add_speed = UnityEngine.Random.Range(1f, 1.6f);
                    ins.transform.localPosition = new Vector3(rnd_x - (i * distance_btw_x), 400, 0);
                    TrapList.Add(ins);
                }
            }

            // CREATE BOUNCE LINE

            var ins2 = Instantiate(Bounce_Line, Traps.transform.localPosition, Quaternion.identity, Traps.transform);

            min = SafeZonesList[SafeZonesList.Count - 2].transform.localPosition.x + 500;
            max = SafeZonesList[SafeZonesList.Count - 1].transform.localPosition.x - 500;
            rnd_x = UnityEngine.Random.Range(min, max);
            rnd_y = UnityEngine.Random.Range(-300, 300);

            ins2.transform.localPosition = new Vector3(rnd_x, rnd_y, 0);
            TrapList.Add(ins2);

        }
        else
        {
            Debug.Log("No data");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {        
        this.GetComponent<Rigidbody2D>().gravityScale = 0f;
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody2D>().angularVelocity = 0;

        col.GetComponent<SafeZoneMove>().enableMove = false;
        col.GetComponent<SafeZoneMove>().enableShrink = false;

        StartCoroutine(MoveCenterOfCheckpoint(col.gameObject));

        int index_local = SafeZonesList.IndexOf(col.gameObject);

        for (int i = 0; i < index_local; i++)
        {
            Destroy(SafeZonesList[i].gameObject);
            SafeZonesList.Remove(SafeZonesList[i]);
        }

        Checkpoint = col.transform.position;

        int index = SafeZonesList.IndexOf(col.gameObject);

        if (SafeZonesList.Count - 1 == SafeZonesList.IndexOf(col.gameObject))
        {
            Create_SafeZones();
            Create_Traps();
        }

        if (col.gameObject.tag != "checkpointzone")
        {
            Calculate_Score();
            current_try = 0;
            level++;
            Level.text = level.ToString();
            SafeZoneSound.Play();
            GameObject.Find("PlanetImg").GetComponent<Animator>().SetTrigger("PlanetImg_Anim");

            if(level < 30)
            {
                time_speed = 3f;
            }
            else if (level < 50)
            {
                time_speed = 3.5f;
            }
            else if (level < 70)
            {
                time_speed = 4f;
            }
            else if (level >= 70)
            {
                time_speed = 4.5f;
            }
        }
      
        current_distance = Mathf.Abs(SafeZonesList[index].transform.localPosition.x - SafeZonesList[index + 1].transform.localPosition.x);

        col.gameObject.tag = "checkpointzone";

        float dist = SafeZonesList[index].transform.localPosition.x + SafeZonesList[index + 1].transform.localPosition.x;
        Cam_Checkpoint.transform.localPosition = new Vector3(dist / 2, 0, 0);

        StartCoroutine(MoveCameratoCheckPoint(Cam_Checkpoint));

        DestroyTraps();
    }

    public void DestroyTraps()
    {
        int destroy_ct = 0;
        for(int i=0; i<TrapList.Count;i++)
        {
            if (TrapList[i].transform.localPosition.x < SafeZonesList[0].transform.localPosition.x)
            {
                destroy_ct++;
            }           
        }

        for (int i = 0; i < destroy_ct; i++)
        {
            Destroy(TrapList[0].gameObject);
            TrapList.Remove(TrapList[0]);
        }
    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "checkpointzone")
        {
            score_decrease = false;
        }
    }

    public void Calculate_Score()
    {
        float current_score = 0f;

        if (current_try <= 1)
        {
            perfect_run++;
            if(SafeZoneSound.pitch <1.5f)
            {
                SafeZoneSound.pitch += 0.05f;
            }            
            current_score = 2000 + (perfect_run * 100);
        }
        else if (current_try == 2)
        {
            perfect_run = 0;
            SafeZoneSound.pitch = 1f;
            current_score = 1000;
        }
        else if (current_try == 3)
        {
            perfect_run = 0;
            SafeZoneSound.pitch = 1f;
            current_score = 700;
        }
        else if (current_try >= 4)
        {
            perfect_run = 0;
            SafeZoneSound.pitch = 1f;
            current_score = 400;
        }
                
        if (perfect_run <= 0)
        {
            Score_PopUp.text = "";
        }
        else if(perfect_run == 1)
        {
            Score_PopUp.text = "GOOD!\nx" + perfect_run;
        }
        else if (perfect_run == 2)
        {
            Score_PopUp.text = "NICE!\nx" + perfect_run;
        }
        else if (perfect_run == 2)
        {
            Score_PopUp.text = "GREAT!\nx" + perfect_run;
        }
        else if (perfect_run == 3)
        {
            Score_PopUp.text = "WELL DONE!\nx" + perfect_run;
        }
        else if (perfect_run == 4)
        {
            Score_PopUp.text = "AWESOME!\nx" + perfect_run;
        }
        else if (perfect_run >= 5)
        {
            Score_PopUp.text = "PERFECT!\nx" + perfect_run;
        }

        if ((level + 1) % 5 == 0 && level != 1)
        {
            score_combo += 0.2f;
            Score_Combo.text = "x" + score_combo.ToString("F1");
        }

        StartCoroutine(Score_Text_Add(current_score));
    }

    IEnumerator Score_Text_Add(float current_score)
    {
        float temp_score = score + ((Mathf.Floor(current_distance) + current_score) * score_combo);
        Score_PopUp.transform.position = Camera.main.WorldToScreenPoint(SafeZonesList[0].transform.position);
        StartCoroutine(Score_PopUp_Position(Score_PopUp.transform.localPosition));

        while (score < temp_score)
        {
            if (score - temp_score <= 3000)
            {
                score += 100;
                Score_Text.text = score.ToString();
                yield return new WaitForSeconds(0.001f);
            }
            else if (score - temp_score <= 1000)
            {
                score += 50;
                Score_Text.text = score.ToString();
                yield return new WaitForSeconds(0.001f);
            }
            else if (score - temp_score <= 100)
            {
                score += 10;
                Score_Text.text = score.ToString();
                yield return new WaitForSeconds(0.001f);
            }
            else if (score - temp_score <= 10)
            {
                score += 1;
                Score_Text.text = score.ToString();
                yield return new WaitForSeconds(0.001f);
            }
        }
        yield return null;
    }

    IEnumerator Score_PopUp_Position(Vector3 start_pos)
    {
        Score_PopUp.gameObject.SetActive(true);
        float timer = 1f;
        start_pos = new Vector3(start_pos.x, start_pos.y + 150, 1);
        while (timer >= 0f)
        {
            start_pos = new Vector3(start_pos.x, start_pos.y + 2, 1);
            Score_PopUp.transform.localPosition = start_pos;
            timer -= 0.03f;
            yield return new WaitForSeconds(0.02f);
        }
        Score_PopUp.gameObject.SetActive(false);

        yield return null;
    }

    IEnumerator MoveCenterOfCheckpoint(GameObject col)
    {
        Vector3 start_pos = this.transform.position;
        Vector3 end_pos = col.transform.position;

        while (start_pos != end_pos)
        {
            this.transform.position = Vector3.Lerp(start_pos, end_pos, 4f * Time.deltaTime);
            start_pos = this.transform.position;

            if (Vector3.Distance(start_pos, end_pos) <= 0.01f)
            {
                start_pos = end_pos;
                this.transform.position = end_pos;
            }
            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine(MoveCameratoCheckPoint(Cam_Checkpoint));
        yield return null;
    }

    IEnumerator MoveCameratoCheckPoint(GameObject cam_checkpoint)
    {
        Vector3 start_pos = MoveCamera.transform.position;
        Vector3 end_pos = cam_checkpoint.transform.position;

        while (start_pos != end_pos)
        {
            MoveCamera.transform.position = Vector3.Lerp(start_pos, end_pos, 2f * Time.deltaTime);
            start_pos = MoveCamera.transform.position;

            if (Vector3.Distance(start_pos, end_pos) <= 0.08f)
            {
                start_pos = end_pos;
                MoveCamera.transform.position = end_pos;
            }
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1f);

        GameObject.Find("Top_Obstacle").GetComponent<BoxCollider2D>().enabled = true;
        GameObject.Find("Bottom_Obstacle").GetComponent<BoxCollider2D>().enabled = true;
        GameObject.Find("Left_Obstacle").GetComponent<BoxCollider2D>().enabled = true;
        GameObject.Find("Right_Obstacle").GetComponent<BoxCollider2D>().enabled = true;

        yield return null;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        this.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        score_decrease = true;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "obs")
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody2D>().angularVelocity = 0;
            this.GetComponent<Image>().enabled = false;

            GameObject.Find("Top_Obstacle").GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("Bottom_Obstacle").GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("Left_Obstacle").GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("Right_Obstacle").GetComponent<BoxCollider2D>().enabled = false;

            this.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            this.transform.GetChild(1).GetComponent<TrailRenderer>().Clear();
            this.transform.GetChild(1).GetComponent<TrailRenderer>().time = 0;

            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            GameOverController.GameOver();

            GameSound.Pause();
            TrapSound.Play();
        }

        else if(coll.gameObject.tag == "bounce")
        {
            BounceSound.Stop();
            BounceSound.Play();            
        }
    }
    
    public void ResetPlayer()
    {
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;

        this.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        this.transform.position = Checkpoint;
        StartCoroutine(MoveCameratoCheckPoint(Cam_Checkpoint));
        this.GetComponent<Image>().enabled = true;
        this.transform.GetChild(1).GetComponent<TrailRenderer>().Clear();
        this.transform.GetChild(1).GetComponent<TrailRenderer>().time = 1;
    }
}
