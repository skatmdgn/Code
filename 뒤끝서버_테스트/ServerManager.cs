using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;
using static BackEnd.BackendAsyncClass;
using System;
using UnityEngine.SceneManagement;
using BackEnd.GlobalSupport;
using UnityEngine.Networking;
using Photon.Chat;
using UnityEngine.UI;

[System.Serializable]
public class ServerManager : MonoBehaviour
{
    public static ServerManager Instance { private set; get; }

    public bool oneStore = false;
    public bool gm = false;

    public ChatClient chatClient;

    public DateTime server_time;
    public DateTime utc_server_time;
    public int last_connected_renewal_time = 0;
    public int token_renewal_time = 0;
    public int servertime_renewal_time = 0;
    public string currentSceneName = string.Empty;

    BackendReturnObject bro = new BackendReturnObject();
    bool isSuccess = false;

    public string client_version;
    public string server_version;
    private int os_type;

    public bool server_loading = false;
    public float server_loading_time;

    public string achievement_table = "achievement";
    public string unit_table = "unit";
    public string collection_table = "collection";
    public string pet_collection_table = "pet_collection";
    public string upgrade_table = "upgrade";
    public string quest_table = "quest";
    public string attendance_table = "attendance";
    public string user_pvp_table = "user_pvp";
    public string language_table = "language";
    public string game_info_table = "game_info";

    public string user_new_boss_table = "user_new_boss_info";
    public string user_rune_table = "user_rune_info";
    public string user_pet_table = "user_pet_info";
    public string user_newbie_attendance_table = "user_newbie_attendance_info";
    public string user_attendance_package_table = "user_attendance_package_info";
    public string user_event_point_info = "user_event_point";
    public string user_event_pay_info = "user_event_pay";
    public string user_event_playTime_info = "user_event_playTime";
    public string user_event_mission_info = "user_event_mission";
    public string user_event_1st_anniversary_info = "Private_table_0";
    public string boss_info = "boss_info";
    public string new_boss_info = "new_boss_info";
    public string ranking_match_table = "Private_table_1";

    private string user_info_table = "user_info";

    private string quest_chart_id = "45870";
    private string unit_chart_id = "46035";
    private string attendance_reward_chart_id = "50408";

    public bool get_achievement_table_check = false;
    public bool get_unit_table_check = false;
    public bool get_collection_table_check = false;
    public bool get_pet_collection_table_check = false;
    public bool get_upgrade_table_check = false;
    public bool get_quest_table_check = false;
    public bool get_attendance_table_check = false;
    public bool get_pvp_user_info_table_check = false;
    //public bool get_exp_table_check = false;
    public bool get_game_info_table_check = false;
    public bool get_boss_info_table_check = false;
    public bool get_user_boss_info_table_check = false;
    public bool get_user_rune_info_table_check = false;
    public bool get_user_pet_info_table_check = false;

    public bool get_user_info_table_check = false;

    public bool check_user_attendance = false;
    public bool check_version = false;
    public bool check_terms = false;
    public bool check_push = false;

    public bool notice = false;
    public bool notice_check = false;

    public bool user_new = false;

    // 크리스마스이벤트 체크
    public bool event_check = false;

    //서버 동기화 코루틴 실행 여부
    public bool is_coroutine_running = false;

    //뒤끝이랑 연결 여부
    public bool is_backend_connected = true;

    // 신규유저 이벤트 체크
    //public bool event_check = false;
    public bool newbie_event_time = false;
    public DateTime newbie_event_start_time;
    public DateTime newbie_event_end_time;

    // 미트 세일 이벤트 체크
    public bool meat_discount_time = false;
    public DateTime meat_discount_start_time;
    public DateTime meat_discount_end_time;

    //포인트 교환 이벤트
    public bool event_point_time;
    public DateTime event_point_start_time;
    public DateTime event_point_end_time;

    //누적 결제 이벤트
    public bool event_pay_time;
    public DateTime event_pay_start_time;
    public DateTime event_pay_end_time;

    //접속 시간 이벤트
    public bool event_playTime_time;
    public DateTime event_playTime_start_time;
    public DateTime event_playTime_end_time;

    //미션 이벤트
    public bool event_mission_time;
    public DateTime event_mission_start_time;
    public DateTime event_mission_end_time;

    //1주년
    public bool event_1st_anniversary_time;
    public DateTime event_1st_anniversary_start_time;
    public DateTime event_1st_anniversary_end_time;

    //블랙프라이데이
    public bool event_blackfriday_time;
    public DateTime event_blackfriday_start_time;
    public DateTime event_blackfriday_end_time;

    public bool ranking_match_time;
    public DateTime ranking_match_start_time;

    //랭킹 uuid
    private string total_stage_ranking = "da9825f0-7a2e-11ea-be16-57cccbe39b6f";
    private string best_stage_ranking = "cf3458a0-7a2e-11ea-9ab2-4f4d511975dc";
    private string power_ranking = "215539b0-7a2a-11ea-be16-57cccbe39b6f";
    private string pvp_ranking = "6133a860-b9c6-11ea-962c-51cc9758171b";
    private string pvp_point = "fa1a9630-dadf-11ea-b6c9-87a46f2f5a7c";
    private string ranking_match_ranking = "cb552930-6c67-11ec-958e-2da87072bc07";


    //로그인 관련

    public string login_type = "null";
    public string guest_id = "";
    public string id_token = "";

    public int server_0 = 0;

    public int server_1 = 0;

    public int server_2 = 0;

    public int client_0 = 0;

    public int client_1 = 0;

    public int client_2 = 0;

    public int error_type = 0;

    public List<string> list_notice;

    public bool network_check;
    public int playTime_update_time = 0;

    public string product;
    // 랭킹 업데이트 시간 관련

    public List<Param> list_check_data_tampering;

    public static WaitForSecondsRealtime secondsRealtime;

    //업데이트문에서 동기화시 타임체커
    public float serverUpdateChecker = 0.0f;
    //업데이트 동기화 진행 중인지 여부
    public bool isServerUpdateChecking = false;

    public bool isPush = true;

    public int ranking_match_count = 0;
    public bool ranking_match_stage_life_set = false;
    public LoadingSceneController loadSceneController;
    int retry = 0;

    private void Awake()
    {
        //server_time = DateTime.Now;
        //PlayerPrefs.DeleteAll();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(Instance.gameObject);
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    void Start()
    {
        oneStore = false; // 원스토어 용일때

        if (PlayerPrefs.HasKey("login_type"))
        {
            login_type = PlayerPrefs.GetString("login_type");
        }
        if (!PlayerPrefs.HasKey("login_type"))
        {
            login_type = "null";
        }
        if (PlayerPrefs.HasKey("guest_id"))
        {
            guest_id = PlayerPrefs.GetString("guest_id");
        }
        if (!PlayerPrefs.HasKey("guest_id"))
        {
            guest_id = Create_guest_id();
        }

        Debug.Log("callbackcallbackcallback");
        // 초기화

        Backend.InitializeAsync(true, callback =>
        {
            if (callback.IsSuccess())
            {
                server_status();
                Server_time_update();

                string googlehash = Backend.Utils.GetGoogleHash();

                //Debug.Log("구글 해시 키 : " + googlehash);
            }
            else
            {
                Debug.Log("서버초기화오류");
            }
        });

        //StartCoroutine("co_server_time_update");

        Network_check();
        secondsRealtime = new WaitForSecondsRealtime(1.0f);
    }



    // Update is called once per frame
    void Update()
    {
        Backend.AsyncPoll();
        //Debug.Log("backendtime: "+);
        //Debug.Log("cccc");
        if (isSuccess)
        {
            isSuccess = false;
            bro.Clear();
            LoadingManager.Instance.Login_panel_close();
            Debug.Log("로그인 성공, IsOnestore : " + oneStore);
            Backend.BMember.GetUserInfo((callback) =>
            {
                Server_time_update();
                if (callback.GetStatusCode() == "200")
                {
                    if (user_new == true) // 새로운 유저 라면
                    {
                        Debug.Log("-- 신규 유저 --");
                        Backend.GameData.Get(user_info_table, new Where(), 100, callback1 =>
                        {
                            if (callback1.GetReturnValuetoJSON()[1].Count == 0)
                            {
                                Insert_userinfo(callback.GetReturnValuetoJSON());
                            }
                        });
                    }
                    else //기존 가입 유저라면
                    {
                        Debug.Log("-- 기존 유저 --");
                        User_nickname_check(callback.GetReturnValuetoJSON());
                    }
                }
            });
        }

        if (get_achievement_table_check == true && get_unit_table_check == true && get_collection_table_check == true && get_upgrade_table_check == true && get_quest_table_check == true
            && get_user_info_table_check == true && check_user_attendance == true && check_version == true && get_attendance_table_check == true && get_pvp_user_info_table_check == true
            && get_game_info_table_check == true && get_boss_info_table_check == true && get_user_boss_info_table_check == true && event_check == true && get_user_rune_info_table_check == true
             && check_terms == true && get_user_pet_info_table_check == true)
        {
            Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@@@@로딩완료@@@@@@@@@@@@@@@@@@@@@@@@@@");
            get_achievement_table_check = false;
            get_unit_table_check = false;
            get_collection_table_check = false;
            get_upgrade_table_check = false;
            get_quest_table_check = false;
            get_user_info_table_check = false;
            check_user_attendance = false;
            get_attendance_table_check = false;
            get_pvp_user_info_table_check = false;
            get_game_info_table_check = false;
            get_boss_info_table_check = false;
            get_user_boss_info_table_check = false;
            get_user_pet_info_table_check = false;
            check_version = false;

            InvokeRepeating("Server_time_update", 300f, 300f);
            InvokeRepeating("CallDBUpdateUserInfoStage", 180f, 180f);
            InvokeRepeating("CallDBUpdateUserLastConnectedTime", 0f, 30f);
            InvokeRepeating("CallDBUpdateUserEventPlayTimeInfo", 120f, 120f);
            InvokeRepeating("CallAccessTokenRenewal", 80000f, 80000f);
            InvokeRepeating("CallCheckAttendance", 60f, 60f);

            Debug.Log("Call Load Scene , user_new" + user_new);
            loadSceneController.gameObject.SetActive(true);
        }
    }

    public void CallDBUpdateUserInfoStage()
    {
        if (network_check)
        {
            DBManager.Instance.DB_Update_user_info_stage();
        }
    }

    public void CallDBUpdateUserLastConnectedTime()
    {
        if (network_check)
        {
            DBManager.Instance.user_info.last_connected_time = server_time.ToString("MM/dd/yyyy H:mm:ss");
            DBManager.Instance.DB_Update_user_last_connected_time();
        }
    }

    public void CallDBUpdateUserEventPlayTimeInfo()
    {
        if (network_check)
        {
            DBManager.Instance.user_event_playTime_info.playTime += 2;
            DBManager.Instance.DB_Update_user_event_playTime_info();

            if (DBManager.Instance.user_info.user_quest_value[1] < DBManager.Instance.list_quest_info[1].quest_target) // 플레이 시간 15분
            {
                DBManager.Instance.user_info.user_quest_value[1] += 2;
            }
            else
            {
                DBManager.Instance.user_info.user_quest_value[1] = 15;
            }

            if (GameManager.Instance.mission_event == true) // 플레이 시간
            {
                if (DBManager.Instance.user_event_mission_info.event_mission_value[2] >= 30)
                {
                    DBManager.Instance.user_event_mission_info.event_mission_value[2] = 30;
                }
                else
                {
                    DBManager.Instance.user_event_mission_info.event_mission_value[2] += 2;
                    UIManager.Instance.event_mission_panel.Mission_panel_setting();
                    DBManager.Instance.user_mission_update_time = 20;
                    DBManager.Instance.DB_Update_user_event_mission_info();
                }
            }
        }
    }

    public void CallAccessTokenRenewal()
    {
        if (network_check)
        {
            token_update();
        }
    }

    public void CallCheckAttendance()
    {
        if (network_check)
        {
            Check_attendance();
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                UIManager.Instance.quest_panel.Quest_panel_check();
                UIManager.Instance.attendance_panel.attendance_panel_check();
                UIManager.Instance.attendance_package_panel.attendance_panel_check();
            }
        }
    }

    public void FixedUpdate()
    {
        server_time = server_time.AddMilliseconds(Time.fixedUnscaledDeltaTime * 1000.0f);

        if (serverUpdateChecker >= 1.0f)
        {
            if (isServerUpdateChecking == false)
            {
                serverUpdateChecker = 0.0f;
                // Debug.LogWarning("servertime : " + server_time + "playtime : " + playTime_update_time);
                //playTime_update_time++;
                //last_connected_renewal_time++;
                //token_renewal_time++;
                //servertime_renewal_time++;        
                ServerTimeUpdate();
            }
        }
        else
        {
            serverUpdateChecker += 1.0f * Time.fixedDeltaTime;
        }
    }

    public void Check_terms()
    {
        if (DBManager.Instance.terms == 0) //약관 동의 안함
        {

        }
        else // 약관 동의함
        {
            check_terms = true;
        }

    }

    public void Check_push()
    {
        Debug.Log("----- Check_push IN -----");
        Debug.Log("SM - push : " + PlayerPrefs.GetInt("push"));
        if (PlayerPrefs.GetInt("push") == 1)
        {
            Set_push_on_off(1); // 푸시 알림 동의
        }
        else
        {
            Set_push_on_off(0); // 푸시 알림 동의 안함
        }
    }

    IEnumerator Load_scene()
    {
        Debug.Log(user_new);
        if (user_new == true) // 새로운 유저라면
        {
            AsyncOperation load = SceneManager.LoadSceneAsync("ComicScene");

            while (!load.isDone)
            {
                yield return null;
            }
        }
        else  // 기존 유저 라면
        {
            AsyncOperation load = SceneManager.LoadSceneAsync("GameScene");

            while (!load.isDone)
            {

                yield return null;
            }
        }
    }

    /*
    public void Stop_server_time_coroutine()
    {
        if (is_coroutine_running == false)
        {
            Debug.LogWarning("===================ReStartCoroutine=================");
            StopCoroutine("co_server_time_update");

            Invoke("Start_server_time_coroutine", 0.1f);
        }
        else
        {
            Server_time_update();
        }
    }
    
    public void Start_server_time_coroutine()
    {
        StartCoroutine("co_server_time_update");
    }
    */
    //뒤끝 연결 상태 체크
    //연결이 안되어있지만 에러 타입이 500번대(뒤끝 서버 에러인 경우는 네트워크 창이 안 뜨게 트루로 반환)
    public bool Is_backend_connect_status()
    {
        Debug.Log("서버 상태 확인");
        //서버연결 체크를 위해 서버에서 임의의 데이터 받아오기
        BackendReturnObject serverCheck = Backend.BMember.GetUserInfo();
        if (serverCheck.GetStatusCode() == "200")
        {
            is_backend_connected = true;
        }
        //뒤끝 연결 오류 이유 체크
        else
        {
            //뒤끝 서버 오류인 경우
            if (serverCheck.IsServerError())
            {
                is_backend_connected = true;
            }
            else
            {
                //Debug.LogWarning("================"+serverCheck.GetStatusCode()+"========================");
                //서버오류가 아닌 다른 이유 중 점검이나 차단된 유저일 경우 강제 종료
                if (serverCheck.GetStatusCode() == "403" || serverCheck.GetStatusCode() == "401")
                {
                    Application.Quit();
                }

                is_backend_connected = false;
            }
        }

        return is_backend_connected;
    }

    public void ServerTimeUpdate()
    {
        isServerUpdateChecking = true;

        //Debug.Log("시간측정");
        DBManager.Instance.random_obscure_achievement_info();
        DBManager.Instance.random_obscure_bossinfo();
        DBManager.Instance.random_obscure_quest_info();
        DBManager.Instance.random_obscure_treasure_info();
        DBManager.Instance.random_obscure_unitinfo();
        DBManager.Instance.random_obscure_userinfo();
        DBManager.Instance.random_obscure_rune_info();
        if (network_check == true)
        {
            Network_check();
        }


        if (last_connected_renewal_time > 30)
        {
            DBManager.Instance.DB_Update_user_last_connected_time();
            last_connected_renewal_time = 0;
        }


        if (playTime_update_time >= 60)
        {
            //Server_connet_check();
            if (event_playTime_time)
            {
                DBManager.Instance.user_event_playTime_info.playTime++;
                DBManager.Instance.DB_Update_user_event_playTime_info();
                playTime_update_time = 0;
            }
            Check_attendance();
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                UIManager.Instance.quest_panel.Quest_panel_check();
                UIManager.Instance.attendance_panel.attendance_panel_check();
                UIManager.Instance.attendance_package_panel.attendance_panel_check();
            }
        }

        if (token_renewal_time > 80000)
        {
            //토큰 갱신
            token_update();
        }

        //서버 시간 갱신 주기 : 3분
        if (servertime_renewal_time > 180)
        {
            Debug.LogWarning("갱신 하기 전 서버시간 : " + server_time);
            servertime_renewal_time = 0;
            Server_time_update();
        }

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (newbie_event_time == true)
            {
                GameManager.Instance.Newbie_Event_check();
                UIManager.Instance.newbie_attendance_panel.newbie_reward_check();
            }
            if (meat_discount_time == true)
            {
                GameManager.Instance.Discount_Event_check();
            }
            if (event_point_time == true)
            {
                GameManager.Instance.Event_point_check();
                UIManager.Instance.event_panel.event_reward_check();
            }
            if (event_pay_time == true)
            {
                GameManager.Instance.Event_pay_check();
                if (UIManager.Instance.event_pay_panel.PayEvent_reward_check())
                {
                    UIManager.Instance.game_panel.pay_event_button_check_icon.gameObject.SetActive(true);
                    UIManager.Instance.game_panel.pay_event_button.image.color = new Color(1, 1, 1, 0);
                    UIManager.Instance.game_panel.pay_event_button_animation.gameObject.SetActive(true);

                    if (UIManager.Instance.menu_panel.isMenuOnOff == false)
                    {
                        UIManager.Instance.menu_panel.menu_button_icon.gameObject.SetActive(true);
                    }
                }
                else
                {
                    UIManager.Instance.game_panel.pay_event_button_check_icon.gameObject.SetActive(false);
                    UIManager.Instance.game_panel.pay_event_button.image.color = new Color(1, 1, 1, 1);
                    UIManager.Instance.game_panel.pay_event_button_animation.gameObject.SetActive(false);
                }
            }
            if (event_playTime_time == true)
            {
                GameManager.Instance.Event_playTime_check();
                if (UIManager.Instance.event_playTime_panel.PlayTimeEvent_reward_check())
                {
                    UIManager.Instance.game_panel.playTime_event_button_check_icon.gameObject.SetActive(true);
                    UIManager.Instance.game_panel.playTime_event_button.image.color = new Color(1, 1, 1, 0);
                    UIManager.Instance.game_panel.playTime_event_button_animation.gameObject.SetActive(true);

                    if (UIManager.Instance.menu_panel.isMenuOnOff == false)
                    {
                        UIManager.Instance.menu_panel.menu_button_icon.gameObject.SetActive(true);
                    }
                }
                else
                {
                    UIManager.Instance.game_panel.playTime_event_button_check_icon.gameObject.SetActive(false);
                    UIManager.Instance.game_panel.playTime_event_button.image.color = new Color(1, 1, 1, 1);
                    UIManager.Instance.game_panel.playTime_event_button_animation.gameObject.SetActive(false);
                }
            }
            if (event_mission_time == true)
            {
                GameManager.Instance.Event_mission_check();
                if (UIManager.Instance.event_mission_panel.Mission_Event_reward_check())
                {
                    UIManager.Instance.game_panel.mission_event_button_check_icon.gameObject.SetActive(true);
                    UIManager.Instance.game_panel.mission_event_button.image.color = new Color(1, 1, 1, 0);
                    UIManager.Instance.game_panel.mission_event_button_animation.gameObject.SetActive(true);

                    if (UIManager.Instance.menu_panel.isMenuOnOff == false)
                    {
                        UIManager.Instance.menu_panel.menu_button_icon.gameObject.SetActive(true);
                    }
                }
                else
                {
                    UIManager.Instance.game_panel.mission_event_button_check_icon.gameObject.SetActive(false);
                    UIManager.Instance.game_panel.mission_event_button.image.color = new Color(1, 1, 1, 1);
                    UIManager.Instance.game_panel.mission_event_button_animation.gameObject.SetActive(false);
                }
            }
            // 설 이벤트
            /*
            if (event_1st_anniversary_time == true)
            {
                GameManager.Instance.Event_1st_anniversary_check();
                //느낌표 및 아이콘 애니메이션
                //미션 및 빙고 보상 수령 가능 여부
                if (UIManager.Instance.event_1st_anniversary_panel.Mission_Event_reward_check())
                {
                    UIManager.Instance.game_panel.anniversary_event_button_check_icon.gameObject.SetActive(true);
                    UIManager.Instance.game_panel.anniversary_event_button.image.color = new Color(1, 1, 1, 0);
                    UIManager.Instance.game_panel.anniversary_event_button_animation.gameObject.SetActive(true);

                    if (UIManager.Instance.menu_panel.isMenuOnOff == false)
                    {
                        UIManager.Instance.menu_panel.menu_button_icon.gameObject.SetActive(true);
                    }
                }
                else
                {
                    UIManager.Instance.game_panel.anniversary_event_button_check_icon.gameObject.SetActive(false);
                    UIManager.Instance.game_panel.anniversary_event_button.image.color = new Color(1, 1, 1, 1);
                    UIManager.Instance.game_panel.anniversary_event_button_animation.gameObject.SetActive(false);
                    //출석 체크 이벤트 보상 수령 가능 여부
                    UIManager.Instance.event_1st_anniversary_panel.Anniversary_attendance_reward_check();
                }
            }
            */

            if (ranking_match_time == true)
            {
                UIManager.Instance.rank_match_button.interactable = true;
            }
            else
            {
                UIManager.Instance.rank_match_button.interactable = false;
            }

            //블랙프라이데이
            if (event_blackfriday_time == true)
            {
                GameManager.Instance.Event_blackfriday_check();
            }

            UIManager.Instance.game_panel.User_item_setting();
            UIManager.Instance.pvp_panel.pvp_time_check();
            UIManager.Instance.ranking_match_panel.ranking_match_time_check();
            UIManager.Instance.rune_panel.Rune_time_check();
            UIManager.Instance.shop_panel.item_ad_time_check();
            UIManager.Instance.shop_panel.treasure_box_ad_time_check();
            UIManager.Instance.shop_panel.rune_ad_time_check();
            UIManager.Instance.save_gold_panel.save_gold_panel_setting();
            UIManager.Instance.post_panel.Post_panel_setting();
            UIManager.Instance.boss_panel.Check_boss_ticket();
            if (UIManager.Instance.pvp_panel.pvp_ticket_buy_panel.activeInHierarchy == true)
                UIManager.Instance.pvp_panel.pvp_ticket_ad_time_check();
            if (ADManager.instance.ad_treasure_check == true)
            {
                if (ADManager.instance.reward_type == 0)
                {
                    UIManager.Instance.ad_reward_particle(0);
                }
                else if (ADManager.instance.reward_type == 1)
                {
                    UIManager.Instance.ad_reward_particle(1);
                }
                else if (ADManager.instance.reward_type == 2)
                {
                    UIManager.Instance.shop_panel.ad_reward_treasure_box();
                }
                else if (ADManager.instance.reward_type == 3)
                {
                    //UIManager.Instance.shop_panel.ad_reward_treasure_box();
                }
                else if (ADManager.instance.reward_type == 4)
                {
                    UIManager.Instance.shop_panel.On_Rune_box(0);
                }
                ADManager.instance.ad_treasure_check = false;
            }
            //ADManager.instance.ad_load_check();
        }

        //Debug.LogWarning("isServerUpdateCheckingOut");
        isServerUpdateChecking = false;
    }

    public void server_status()
    {

        Debug.Log("----- server_status IN -----");
        Backend.Utils.GetServerStatus(callback =>
        {
            Debug.Log(callback.GetStatusCode());
            Debug.Log(callback.GetMessage());
            JsonData json = JsonMapper.ToObject(callback.GetReturnValue());

            if (json["serverStatus"].ToString() == "0")
            {
                Get_notice();
            }
            else if (json["serverStatus"].ToString() == "2")
            {
                LoadingManager.Instance.public_popup_panel.On_popup_setting("점검중입니다. 최대한 빨리 정상화하도록 하겠습니다.");
            }
        });
    }
    // 임시 공지
    public void Get_notice()
    {
        Debug.Log("----- Get_notice IN -----");

        if (notice_check == false)
        {
            Debug.Log("임시공지 시작");
            Backend.Notice.GetTempNotice(callback =>
            {
                //Debug.LogError(callback);
                JsonData json = JsonMapper.ToObject(callback);
                if(callback == string.Empty)
                {
                    if(retry < 5)
                    {
                        Debug.LogError(retry);
                        retry++;
                        Get_notice();
                    }
                }
                else
                {
                    notice_check = true;
                    retry = 0;
                    if (json["isUse"].ToString() == "False")
                    {
                        //Debug.LogError(json["isUse"].ToString());
                        Debug.Log("-- 임시 공지 미사용 --");
                        GetLatestVersion();
                    }
                    else
                    {
                        Debug.Log("-- 임시 공지 사용 --");
                        char[] splitter = { ',' };

                        string[] splitter_notice = json["contents"].ToString().Split(splitter);
                        for (int i = 0; i < splitter_notice.Length; i++)
                        {
                            list_notice.Add(splitter_notice[i]);
                        }
                        LoadingManager.Instance.public_popup_panel.On_popup_setting(list_notice[DBManager.Instance.language_index]);
                    }
                }
            });
        }
    }

    // 최신 버전 가져오기
    public void GetLatestVersion()
    {
        Debug.Log("----- GetLatestVersion IN -----");
        Backend.Utils.GetLatestVersion(callback =>
        {
            DoLatestVersion(callback);
        });
    }
    private void DoLatestVersion(BackendReturnObject bro)
    {
        Debug.Log("-- DOLatesVersion IN --");
#if UNITY_ANDROID
        if (bro.IsSuccess())
        {
            server_version = bro.GetReturnValuetoJSON()["version"].ToString();
            os_type = (int)bro.GetReturnValuetoJSON()["type"];

            int server_0 = int.Parse(server_version.Substring(0, 1));
            Debug.Log("version" + server_0);
            server_1 = int.Parse(server_version.Substring(2, 2));
            Debug.Log("version" + server_1);
            server_2 = int.Parse(server_version.Substring(5, 1));
            Debug.Log("version" + server_2);
            client_0 = int.Parse(client_version.Substring(0, 1));
            Debug.Log("version" + client_0);
            client_1 = int.Parse(client_version.Substring(2, 2));
            Debug.Log("version" + client_1);
            client_2 = int.Parse(client_version.Substring(5, 1));
            Debug.Log("version" + client_2);

            if (server_version == "0") // 점검중
            {
                Debug.Log("점검중");
                LoadingManager.Instance.public_popup_panel.On_popup_setting("107");
            }
            else
            {
                if (server_0 < client_0) // 버전이 같다면
                {
                    check_version = true;
                }
                else if (server_0 == client_0)
                {
                    if (server_1 < client_1)
                    {
                        check_version = true;
                    }
                    else if (server_1 == client_1)
                    {
                        if (server_2 <= client_2)
                        {
                            check_version = true;
                        }
                        else
                        {
                            LoadingManager.Instance.version_popup_panel.Popup_setting(DBManager.Instance.Language_converter(664) + "\n" + DBManager.Instance.Language_converter(665) + "\n" + DBManager.Instance.Language_converter(666));
                        }
                    }
                    else
                    {
                        LoadingManager.Instance.version_popup_panel.Popup_setting(DBManager.Instance.Language_converter(664) + "\n" + DBManager.Instance.Language_converter(665) + "\n" + DBManager.Instance.Language_converter(666));
                    }
                }
                else// 버전이 다르다면
                {
                    LoadingManager.Instance.version_popup_panel.Popup_setting(DBManager.Instance.Language_converter(664) + "\n" + DBManager.Instance.Language_converter(665) + "\n" + DBManager.Instance.Language_converter(666));
                }

            }
            if (check_version == true) // 버전이 같거나 높으면
            {
                if (login_type == "null") // 로그인 정보가 없으면
                {
                    Debug.Log("-- 로그인 정보 없음 --");
                    LoadingManager.Instance.Login_panel_open();
                }
                else
                {
                    Debug.Log("-- 로그인 정보 있음 --");
                    Auto_login();
                }
            }
        }
        else
        {

        }
#endif
#if UNITY_IOS
         if (bro.IsSuccess())
        {
             server_version = bro.GetReturnValuetoJSON()["version"].ToString();
             os_type = (int)bro.GetReturnValuetoJSON()["type"];

            int server_0 = int.Parse(server_version.Substring(0, 1));
            Debug.Log("version" + server_0);
            server_1 = int.Parse(server_version.Substring(2, 2));
            Debug.Log("version" + server_1);
            server_2 = int.Parse(server_version.Substring(5, 1));
            Debug.Log("version" + server_2);
            client_0 = int.Parse(client_version.Substring(0, 1));
            Debug.Log("version" + client_0);
            client_1 = int.Parse(client_version.Substring(2, 2));
            Debug.Log("version" + client_1);
            client_2 = int.Parse(client_version.Substring(5, 1));
            Debug.Log("version" + client_2);

            if (server_version == "0") // 점검중
            {
                Debug.Log("점검중");
                LoadingManager.Instance.public_popup_panel.On_popup_setting("107");
            }
            else
            {
                if (server_0 < client_0) // 버전이 같다면
                {
                    ServerManager.Instance.check_version = true;
                }
                else if (server_0 == client_0)
                {
                    if (server_1 < client_1)
                    {
                        ServerManager.Instance.check_version = true;
                    }
                    else if (server_1 == client_1)
                    {
                        if (server_2 <= client_2)
                        {
                            ServerManager.Instance.check_version = true;
                        }
                        else
                        {
                            LoadingManager.Instance.version_popup_panel.Popup_setting(DBManager.Instance.List_language[DBManager.Instance.language_index][77]);
                        }

                    }
                    else
                    {
                        LoadingManager.Instance.version_popup_panel.Popup_setting(DBManager.Instance.List_language[DBManager.Instance.language_index][77]);
                    }
                }
                else// 버전이 다르다면
                {
                    LoadingManager.Instance.version_popup_panel.Popup_setting(DBManager.Instance.List_language[DBManager.Instance.language_index][77]);
                }
            }
            if (ServerManager.Instance.check_version == true)
            {
                if (login_type == "null")// 로그인 정보가 없으면
                {
                    LoadingManager.Instance.Login_panel_open();
                }
                else
                {
                    Auto_login();
                }
            }
        }
        else
        {
            
        }
#endif
#if UNITY_EDITOR
        server_version = client_version;
        if (server_version == "0") // 점검중
        {
            LoadingManager.Instance.public_popup_panel.On_popup_setting("107");
        }
        else
        {
            if (server_version == client_version) // 버전이 같다면
            {
                ServerManager.Instance.check_version = true;
            }
            else // 버전이 다르다면
            {
                LoadingManager.Instance.version_popup_panel.Popup_setting(DBManager.Instance.List_language[DBManager.Instance.language_index][77]);
            }
        }
        if (ServerManager.Instance.check_version == true)
        {
            if (login_type == "null")// 로그인 정보가 없으면
            {
                LoadingManager.Instance.Login_panel_open();
            }
            else
            {
                Auto_login();
            }
        }
#endif
    }
    public void Auto_login()
    {
        Debug.Log("----- Auto_login IN -----");
        Debug.Log("버전이 다름00");
        if (login_type == "google")
        {
            Debug.Log("-- google --");
            On_google_login_button();
        }
        else if (login_type == "apple")
        {
            Debug.Log("-- apple --");
            On_apple_login_button();
        }
        else if (login_type == "guest")
        {
            Debug.Log("-- guest --");
            On_guest_login_button();
        }
    }
    public void token_update()
    {
        Debug.Log("----- token_update IN -----");
        Backend.BMember.RefreshTheBackendToken((callback) =>
        {
            Debug.Log("token update" + callback.GetStatusCode());
            if (callback.GetStatusCode() == "201")
            {
                Debug.Log("-- 토큰 갱신 성공 --");
                token_renewal_time = 0;
            }
            else
            {
                Debug.Log("-- 토큰 갱신 실패 --");
                token_update();
            }
        });
    }
    public void User_nickname_check(JsonData json)
    {
        Debug.Log("----- User_nickname_check1 IN -----");
        if (json[0]["nickname"] == null)
        {
            Debug.Log("닉네임 설정 팝업");
            LoadingManager.Instance.Nickname_popup_open();
        }
        else
        {
            Debug.Log("-- 닉네임 존재 --");
            DBManager.Instance.user_info.nickname = json[0]["nickname"].ToString();
            Get_notice_list();
        }
    }
    // 닉네임 패널에서 닉네임 제한 사항 확인
    public void User_nickname_check(string nickname) // 중복 //19자이하 // 앞뒤 공백
    {
        Debug.Log("----- User_nickname_check2 IN -----");
        bool nickname_check_0 = false;
        bool nickname_check_1 = false;

        if (nickname.Length > 1)
        {
            nickname_check_0 = true;
        }
        else
        {
            nickname_check_0 = false;
        }
        if (nickname.Contains(" "))
        {
            nickname_check_1 = false;
        }
        else
        {
            nickname_check_1 = true;
        }
        if (nickname_check_0 == true && nickname_check_1 == true)
        {
            Backend.BMember.CheckNicknameDuplication(nickname, (callback) =>
            {
                Debug.Log(callback);
                if (callback.GetStatusCode() == "204")
                {
                    LoadingManager.Instance.Nickname_popup_close();
                    User_nickname_insert(nickname);

                }
                else if (callback.GetStatusCode() == "409")
                {
                    LoadingManager.Instance.public_popup_panel.On_popup_setting(DBManager.Instance.List_language[DBManager.Instance.language_index][81]);
                }
                else if (callback.GetStatusCode() == "400")
                {
                    LoadingManager.Instance.public_popup_panel.On_popup_setting(DBManager.Instance.List_language[DBManager.Instance.language_index][106]);
                }
                else
                {
                    Server_connect_check();
                }
            });
            Debug.Log(nickname);
        }
        else
        {
            LoadingManager.Instance.public_popup_panel.On_popup_setting(DBManager.Instance.List_language[DBManager.Instance.language_index][106]);
        }
    }
    public void User_nickname_insert(string nickname)
    {
        BackendReturnObject nickname_insert = Backend.BMember.CreateNickname(nickname);
        Debug.Log(nickname_insert.GetStatusCode());
        if (nickname_insert.GetStatusCode() == "204")
        {
            Debug.Log("닉네임 설정 완료");
            DBManager.Instance.user_info.nickname = nickname;
            //DBManager.Instance.DB_Update_user_info();
            Get_notice_list();
        }
        else if (nickname_insert.GetStatusCode() == "409")
        {
            Debug.Log("중복된 닉네임이 있음");
        }
        else if (nickname_insert.GetStatusCode() == "400")
        {
            Debug.Log("20자 이상의 닉네임인 경우");
            Debug.Log("닉네임 앞/뒤 공백이 있는 경우");
        }
        else
        {
            Server_connect_check();
        }
    }
    public void Update_nickname(string nickname)
    {
        bool nickname_check_0 = false;
        bool nickname_check_1 = false;

        if (nickname.Length > 1)
        {
            nickname_check_0 = true;
        }
        else
        {
            nickname_check_0 = false;
        }
        if (nickname.Contains(" "))
        {
            nickname_check_1 = false;
        }
        else
        {
            nickname_check_1 = true;
        }
        if (nickname_check_0 == true && nickname_check_1 == true)
        {
            Backend.BMember.CheckNicknameDuplication(nickname, (callback) =>
            {
                Debug.Log(callback.GetStatusCode());
                Debug.Log(callback.GetMessage());
                if (callback.GetStatusCode() == "204")
                {
                    Backend.BMember.UpdateNickname(nickname, (callback1) =>
                    {
                        if (callback1.GetStatusCode() == "204")
                        {
                            Debug.Log("닉네임 설정 완료");
                            DBManager.Instance.user_info.diamond -= 50;
                            DBManager.Instance.user_info.nickname = nickname;
                            DBManager.Instance.DB_Update_user_info();
                            UIManager.Instance.setting_panel.user_nickname_text.text = DBManager.Instance.Language_converter(39) + " : " + DBManager.Instance.user_info.nickname;
                            UIManager.Instance.setting_panel.Off_nickname_change_panel_button();
                        }
                        else if (callback.GetStatusCode() == "400")
                        {
                            Debug.Log("20자 이상의 닉네임인 경우");
                            Debug.Log("닉네임 앞/뒤 공백이 있는 경우");
                            UIManager.Instance.notice_popup_panel.On_popup_setting(DBManager.Instance.List_language[DBManager.Instance.language_index][106]);
                        }
                    });

                }
                else if (callback.GetStatusCode() == "409")
                {
                    Debug.Log("중복된 닉네임이 있음");
                    UIManager.Instance.notice_popup_panel.On_popup_setting(DBManager.Instance.List_language[DBManager.Instance.language_index][81]);
                }
                else if (callback.GetStatusCode() == "400")
                {
                    Debug.Log("20자 이상의 닉네임인 경우");
                    Debug.Log("닉네임 앞/뒤 공백이 있는 경우");
                    UIManager.Instance.notice_popup_panel.On_popup_setting(DBManager.Instance.List_language[DBManager.Instance.language_index][106]);
                }
                else
                {
                    Server_connect_check();
                }
            });
        }
        else
        {
            UIManager.Instance.notice_popup_panel.On_popup_setting(DBManager.Instance.List_language[DBManager.Instance.language_index][106]);
        }
    }
    public void On_guest_login_button()
    {
        Guest_signup(guest_id, "Fix@0816");
        LoadingManager.Instance.Guest_login_popup_close();
    }
    public void On_google_login_button()
    {
        SocialManager.instance.OnSignIn("login");
    }
    public void On_google_connect_button()
    {
        SocialManager.instance.OnSignIn("connect");
    }
    public void On_apple_login_button()
    {
#if UNITY_IOS
                    Debug.Log("-----------------apple--------------");
                    SocialManager.instance.SignInWithApple("login");
#endif
    }
    public void On_apple_connect_button()
    {
#if UNITY_IOS
        SocialManager.instance.SignInWithApple("connect");
#endif
    }
    public void Guest_signup(string id, string pw)
    {
        Backend.BMember.CustomSignUp(id, pw, (callback) =>
        {
            Debug.Log(callback.GetMessage());
            callback.GetMessage();
            if (callback.GetStatusCode() == "201")
            {
                user_new = true;
                PlayerPrefs.SetString("login_type", "guest");
                ServerManager.Instance.login_type = "guest";
                PlayerPrefs.SetString("guest_id", id);
                DBManager.Instance.user_info.id = id;
            }
            else if (callback.GetStatusCode() == "409")
            {
                Guest_login(id, pw);
            }
            else if (callback.GetStatusCode() == "401")
            {
                // 비밀번호가 틀렸습니다.
            }
            else if (callback.GetStatusCode() == "403")
            {
                callback.GetMessage();
            }
            else
            {
                Server_connect_check();
            }
            isSuccess = callback.IsSuccess();
            bro = callback;
        });
        Debug.Log("Guest_signup <<<<<<<<<<<<<<<<<<<<<");
    }
    public void Guest_login(string id, string pw)
    {
        Debug.Log("Guest_login >>>>>>>>>>>>>>>>>>>>>");
        Backend.BMember.CustomLogin(id, pw, (callback) =>
        {
            Debug.Log("CustomLogin " + callback);
            bro = callback;
            if (callback.GetStatusCode() == "200")  // 로그인 완료
            {
                isSuccess = callback.IsSuccess();
                bro = callback;
                PlayerPrefs.SetString("login_type", "guest");
                ServerManager.Instance.login_type = "guest";
                PlayerPrefs.SetString("guest_id", id);
                DBManager.Instance.user_info.id = id;
            }
            else if (callback.GetStatusCode() == "409")
            {
                // 동일한 아이디가 존재 합니다.
            }
            else if (callback.GetStatusCode() == "401")
            {
                // 비밀번호가 틀렸습니다.
            }
            else if (callback.GetStatusCode() == "403")
            {
                Open_cut_popup(callback.GetErrorCode());
            }
            else
            {
                Server_connect_check();
            }
        });
        Debug.Log("Guest_login <<<<<<<<<<<<<<<<<<<<<");
    }
    public string Create_guest_id()
    {
        string guest_id = "" + DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd");
        for (int i = 0; i < 7; i++)
        {
            guest_id += UnityEngine.Random.Range(0, 9);
        }
        Debug.Log(guest_id);
        return guest_id;
    }
    public void Google_login(string token, string id)
    {
        Debug.Log("Google_login");

        Backend.BMember.AuthorizeFederation(token, FederationType.Google, callback =>
        {
            Debug.Log("-----------------google login------------------" + callback.GetReturnValue());
            if (callback.GetStatusCode() == "200") // 이미 가입한 회원
            {
                login_type = "google";
                id_token = token;
                PlayerPrefs.SetString("login_type", "google");
                PlayerPrefs.SetString("goolge_id", id);
                DBManager.Instance.user_info.id = id;
            }
            else if (callback.GetStatusCode() == "201") // 신규 가입한 회원
            {
                login_type = "google";
                PlayerPrefs.SetString("login_type", "google");
                PlayerPrefs.SetString("goolge_id", id);
                DBManager.Instance.user_info.id = id;
                user_new = true;
            }
            else if (callback.GetStatusCode() == "403")
            {
                Open_cut_popup(callback.GetErrorCode());
            }
            isSuccess = true;
            bro = callback;
        });

    }
    public void Google_connect(string Token, string id)
    {
        Debug.Log("on Google_connect");
        Backend.BMember.ChangeCustomToFederation(Token, FederationType.Google, callback =>
        {
            //이후 처리
            Debug.Log("-----------------google_connect------------------" + callback.GetReturnValue());
            if (callback.GetStatusCode() == "204") // 페러데이션 연동 성공
            {
                login_type = "google";
                id_token = Token;
                Debug.Log("----------------------연동 성공-------------------");
                PlayerPrefs.SetString("login_type", "google");
                PlayerPrefs.SetString("goolge_id", id);
                DBManager.Instance.user_info.id = id;
                UIManager.Instance.setting_panel.On_setting_on_button();
            }
            else if (callback.GetStatusCode() == "409") //이미 가입되어 있는 아이디
            {
                SocialManager.instance.Google_sign_out();
                UIManager.Instance.On_public_popup_panel("Duplicated federationId , 이미 가입된 이메일입니다");
                Debug.Log("---------------------이미 가입되어 있는 아이디----------------");
            }
        });


    }
    public void Apple_connect(string token, string id)
    {
        Debug.Log("on apple_connect");
        Backend.BMember.ChangeCustomToFederation(token, FederationType.Apple, callback =>
        {
            //이후 처리
            Debug.Log("-----------------apple_connect------------------" + callback.GetReturnValue());
            if (callback.GetStatusCode() == "204") // 페러데이션 연동 성공
            {
                login_type = "apple";
                id_token = token;
                Debug.Log("----------------------연동 성공-------------------");
                PlayerPrefs.SetString("login_type", "apple");
                PlayerPrefs.SetString("apple_id", id);
                DBManager.Instance.user_info.id = id;
                UIManager.Instance.setting_panel.On_setting_on_button();
            }
            else if (callback.GetStatusCode() == "409") //이미 가입되어 있는 아이디
            {
                UIManager.Instance.On_public_popup_panel("Duplicated federationId , 이미 가입된 이메일입니다");
                Debug.Log("---------------------이미 가입되어 있는 아이디----------------");
            }
        });
    }
    public void Re_login()
    {
        Debug.Log("-Re_login-");
        if (login_type == "google")
        {
            Backend.BMember.AuthorizeFederation(id_token, FederationType.Google, callback =>
            {
                if (callback.GetStatusCode() == "200")  // 로그인 완료
                {
                    sned_log_login("재접속");
                }
                else
                {
                    error_type = 37;
                    Server_connect_check();
                }
                Debug.Log("re_googleLogin " + callback.GetStatusCode());
            });
        }
        else if (login_type == "apple")
        {
            Backend.BMember.AuthorizeFederation(id_token, FederationType.Apple, callback =>
            {
                if (callback.GetStatusCode() == "200")  // 로그인 완료
                {
                    sned_log_login("재접속");
                }
                else
                {
                    error_type = 37;
                    Server_connect_check();
                }
                Debug.Log("re_apple_Login " + callback.GetStatusCode());
            });
        }
        else if (login_type == "guest")
        {
            Backend.BMember.CustomLogin(PlayerPrefs.GetString(guest_id), "Fix@0816", (callback) =>
            {
                Debug.Log("re_CustomLogin " + callback.GetStatusCode());

                if (callback.GetStatusCode() == "200")  // 로그인 완료
                {
                    sned_log_login("재접속");
                }
                else
                {
                    error_type = 37;
                    Server_connect_check();
                }
            });
        }
    }
    public void Apple_login(string token, string id)
    {
        Debug.Log("apple_login");

        Backend.BMember.AuthorizeFederation(token, FederationType.Apple, callback =>
        {
            Debug.Log("-----------------Apple_login------------------" + callback.GetStatusCode());
            if (callback.GetStatusCode() == "200") // 이미 가입한 회원
            {
                login_type = "apple";
                id_token = token;
                PlayerPrefs.SetString("login_type", "apple");
                PlayerPrefs.SetString("apple_id", id);
                DBManager.Instance.user_info.id = id;
            }
            else if (callback.GetStatusCode() == "201") // 신규 가입한 회원
            {
                login_type = "apple";
                PlayerPrefs.SetString("login_type", "apple");
                PlayerPrefs.SetString("apple_id", id);
                DBManager.Instance.user_info.id = id;
                user_new = true;
            }
            else if (callback.GetStatusCode() == "403")
            {
                Open_cut_popup(callback.GetErrorCode());
            }
            isSuccess = true;
            bro = callback;
        });
    }
    public void Log_out()
    {
        Debug.Log("-Log_out-");
        Backend.BMember.Logout((callback) =>
        {
            Debug.Log(callback.GetStatusCode());
            Debug.Log(callback.GetMessage());

            if (callback.GetStatusCode() == "204")
            {

                login_type = "null";
                PlayerPrefs.DeleteKey("login_type");
                SceneManager.LoadScene("LoadingScene");
            }
            // 이후 처리
        });
    }
    public void Open_cut_popup(JsonData json)
    {
        LoadingManager.Instance.cut_popup_panel.On_cut_popup_setting(json.ToString());
    }

    public void Get_achievement_info()
    {
        Debug.Log("----- Get_achievement_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(151);

        Backend.GameData.Get(achievement_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 업적 정보 가져오기 성공 --");
                DBManager.Instance.DB_Insert_achievement_info(callback.GetReturnValuetoJSON());

                Get_unit_info();
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 업적 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 0;
                Server_connect_check();
            }
        });

    }

    public void Get_unit_info()
    {

        Debug.Log("----- Get_unit_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(152);

        // 차트 비동기
        /*
        Backend.Chart.GetChartContents(unit_chart_id, (callback) =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 유닛 정보 가져오기 성공 --");
                LoadingManager.Instance.loading_bar_setting(2);
                DBManager.Instance.DB_Insert_unit_info(callback.GetReturnValuetoJSON());
                Get_upgrade_info();
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("-- 유닛 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 1;
                Server_connect_check();
            }
        });
        */
        // 차트 동기
        BackendReturnObject bro = Backend.Chart.GetChartContents(unit_chart_id);
        if (bro.GetStatusCode() == "200")
        {
            Debug.Log("-- 유닛 정보 가져오기 성공 --");
            LoadingManager.Instance.loading_bar_setting(2);
            DBManager.Instance.DB_Insert_unit_info(bro.GetReturnValuetoJSON());
            Get_upgrade_info();
        }
        else if (bro.GetStatusCode() == "400")
        {
            Debug.Log("-- 유닛 정보 가져오기 실패 --");
            server_loading = false;
            error_type = 1;
            Server_connect_check();
        }
    }
    public void Get_upgrade_info()
    {
        Debug.Log("----- Get_upgrade_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(153);

        Backend.GameData.Get(upgrade_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 업그레이드 정보 가져오기 성공 --");
                LoadingManager.Instance.loading_bar_setting(3);
                DBManager.Instance.DB_Insert_upgrade_info(callback.GetReturnValuetoJSON());
                Get_collection_info();
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 업그레이드 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 2;
                Server_connect_check();
            }

        });
    }
    public void Get_collection_info()
    {
        Debug.Log("----- Get_collection_info IN -----");
        Backend.GameData.Get(collection_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 도감 정보 가져오기 성공 --");
                LoadingManager.Instance.loading_bar_setting(4);
                DBManager.Instance.DB_Insert_collection_info(callback.GetReturnValuetoJSON());
                Get_pet_collection_info();
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 도감 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 3;
                Server_connect_check();
            }

        });
    }

    public void Get_pet_collection_info()
    {
        Debug.Log("----- Get_pet_collection_info IN -----");
        Backend.GameData.Get(pet_collection_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 신수 도감 정보 가져오기 성공 --");
                LoadingManager.Instance.loading_bar_setting(4);
                DBManager.Instance.DB_Insert_pet_collection_info(callback.GetReturnValuetoJSON());
                Get_quest_info();
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 신수 도감 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 40;
                Server_connect_check();
            }

        });
    }
    public void Get_quest_info()
    {
        Debug.Log("----- Get_quest_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(154);

        Backend.Chart.GetChartContents(quest_chart_id, (callback) =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 일일 임무 정보 가져오기 성공 --");
                LoadingManager.Instance.loading_bar_setting(5);
                DBManager.Instance.DB_Insert_quest_info(callback.GetReturnValuetoJSON());
                Get_attendance_info();
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("-- 일일 임무 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 4;
                Server_connect_check();
            }
        });
        /*
        Backend.GameData.Get(quest_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 일일 임무 정보 가져오기 성공 --");
                LoadingManager.Instance.loading_bar_setting(5);
                DBManager.Instance.DB_Insert_quest_info(callback.GetReturnValuetoJSON());
                Get_attendance_info();
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 일일 임무 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 4;
                Server_connect_check();
            }
        });
        */
    }
    public void Get_attendance_info()
    {
        Debug.Log("----- Get_attendance_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(155);

        // 출석 보상 정보 차트로 가져오기
        Backend.Chart.GetChartContents(attendance_reward_chart_id, callback => 
        {
            if(callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 출석 정보 가져오기 성공 --");
                LoadingManager.Instance.loading_bar_setting(6);
                DBManager.Instance.DB_Insert_attendance_info(callback.GetReturnValuetoJSON());
                Get_user_info();
            }
            else if(callback.GetStatusCode() == "400")
            {
                Debug.Log("-- 출석 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 5;
                Server_connect_check();
            }
        });

        /*
        Backend.GameData.Get(attendance_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 출석 정보 가져오기 성공 --");
                LoadingManager.Instance.loading_bar_setting(6);
                DBManager.Instance.DB_Insert_attendance_info(callback.GetReturnValuetoJSON());
                Get_user_info();
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 출석 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 5;
                Server_connect_check();
            }
        });
        */
    }
    public void Get_user_info()
    {
        Debug.Log("----- Get_user_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(156);

        Backend.GameData.Get(user_info_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 유저 정보 가져오기 성공 --");
                sned_log_login(callback.GetReturnValue());
                LoadingManager.Instance.loading_bar_setting(7);
                DBManager.Instance.DB_Insert_user_info(callback.GetReturnValuetoJSON());
                Get_pvp_user_info();
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 유저 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 6;
                Server_connect_check();
            }
        });
    }
    public void Get_pvp_user_info()
    {
        Debug.Log("----- Get_pvp_user_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(157);

        Backend.GameData.GetMyData(user_pvp_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 유저 PVP 정보 가져오기 성공 --");
                LoadingManager.Instance.loading_bar_setting(8);
                DBManager.Instance.DB_Insert_pvp_user_info(callback.GetReturnValuetoJSON());
                Get_game_info();
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 유저 PVP 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 7;
                Server_connect_check();
            }
        });
    }

    public void Get_game_info()
    {
        Debug.Log("----- Get_game_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(157);

        Backend.GameData.Get(game_info_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 공용 게임 정보 가져오기 성공 --");
                LoadingManager.Instance.loading_bar_setting(10);
                DBManager.Instance.DB_Insert_game_info(callback.GetReturnValuetoJSON());
                Get_boss_info();
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 공용 게임 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 8;
                Server_connect_check();
            }
        });

    }
    public void Get_boss_info()
    {
        Debug.Log("----- Get_boss_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(157);

        Backend.GameData.Get(new_boss_info, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 보스 정보 가져오기 성공 --");
                LoadingManager.Instance.loading_bar_setting(10);
                DBManager.Instance.DB_Insert_boss_info(callback.GetReturnValuetoJSON());
                Get_user_ranking_match_info(); // 랭킹전 유저 정보 가져오기
                Get_user_boss_info();
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 보스 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 16;
                Server_connect_check();
            }
        });
    }

    public void Get_user_boss_info()/////////////////////////////////////////
    {
        Debug.Log("----- Get_user_boss_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(157);

        Backend.GameData.GetMyData(user_new_boss_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                LoadingManager.Instance.loading_bar_setting(10);
                if (callback.GetReturnValuetoJSON()[1].Count == 0)
                {
                    Debug.Log("-- 유저 보스 정보 없음 --");
                    Insert_new_user_boss();
                }
                else
                {
                    Debug.Log("-- 유저 보스 정보 가져오기 성공 --");
                    DBManager.Instance.DB_Insert_user_boss_info(callback.GetReturnValuetoJSON());
                    Get_user_rune_info();
                }
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 유저 보스 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 17;
                Server_connect_check();
            }

        });
    }
    public void Get_user_rune_info()
    {
        Debug.Log("----- Get_user_rune_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(157);

        Backend.GameData.GetMyData(user_rune_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                sned_log_login(callback.GetReturnValue());
                LoadingManager.Instance.loading_bar_setting(10);
                if (callback.GetReturnValuetoJSON()[1].Count == 0)
                {
                    Debug.Log("-- 유저 룬 정보 없음 --");
                    Insert_user_rune_info();
                }
                else
                {
                    DBManager.Instance.DB_Insert_user_rune_info(callback.GetReturnValuetoJSON());
                    Get_user_pet_info();
                }
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                server_loading = false;
                error_type = 25;
                Server_connect_check();
            }

        });
    }
    public void Get_user_pet_info()
    {
        Debug.Log("----- Get_user_pet_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(157);

        Backend.GameData.GetMyData(user_pet_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                sned_log_login(callback.GetReturnValue());
                LoadingManager.Instance.loading_bar_setting(10);
                if (callback.GetReturnValuetoJSON()[1].Count == 0)
                {
                    Debug.Log("-- 유저 펫 정보 없음 --");
                    Insert_user_pet_info();
                }
                else
                {
                    Debug.Log("-- 유저 펫 정보 가져오기 성공 --");
                    DBManager.Instance.DB_Insert_user_pet_info(callback.GetReturnValuetoJSON());
                    Get_user_newbie_attendance_info();
                }
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 유저 펫 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 25;
                Server_connect_check();
            }
        });
    }
    public void Get_user_newbie_attendance_info()
    {
        Debug.Log("----- Get_user_newbie_attendance_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(157);

        Backend.GameData.GetMyData(user_newbie_attendance_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                LoadingManager.Instance.loading_bar_setting(10);

                if (callback.GetReturnValuetoJSON()[1].Count == 0)
                {
                    Debug.Log("-- 신규 유저 출석 이벤트 정보 없음 --");
                    Insert_user_newbie_attendance();
                }
                else
                {
                    Debug.Log("-- 신규 유저 출석 이벤트 정보 가져오기 성공 --");
                    DBManager.Instance.DB_Insert_user_newbie_attendance_info(callback.GetReturnValuetoJSON());
                    Get_user_attendance_package_info();
                }
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 신규 유저 출석 이벤트 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 17;
                Server_connect_check();
            }

        });
    }
    public void Get_user_attendance_package_info()
    {
        Debug.Log("----- Get_user_attendance_package_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(157);

        Backend.GameData.GetMyData(user_attendance_package_table, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                LoadingManager.Instance.loading_bar_setting(10);
                if (callback.GetReturnValuetoJSON()[1].Count == 0)
                {
                    Debug.Log("-- 신규 유저 출석 이벤트 정보 없음 --");
                    Insert_user_attendance_package();
                }
                else
                {
                    Debug.Log("-- 신규 유저 출석 이벤트 정보 가져오기 성공 --");
                    DBManager.Instance.DB_Insert_user_attendance_package_info(callback.GetReturnValuetoJSON());
                    Get_user_event_info();
                }
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 신규 유저 출석 이벤트 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 38;
                Server_connect_check();
            }

        });
    }
    public void Get_user_event_info()
    {
        Debug.Log("----- Get_user_event_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(157);

        Backend.GameData.GetMyData(user_event_point_info, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                LoadingManager.Instance.loading_bar_setting(10);
                if (callback.GetReturnValuetoJSON()[1].Count == 0)
                {
                    Debug.Log("-- 이벤트 정보 없음 --");
                    Insert_user_event_info();
                }
                else
                {
                    Debug.Log("-- 이벤트 정보 가져오기 성공 --");
                    DBManager.Instance.DB_Insert_user_event_info(callback.GetReturnValuetoJSON());
                    Get_user_event_pay_info();
                }
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 이벤트 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 33;
                Server_connect_check();
            }
        });
    }
    public void Get_user_event_pay_info()
    {
        Debug.Log("----- Get_user_event_pay_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(157);

        Backend.GameData.GetMyData(user_event_pay_info, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                LoadingManager.Instance.loading_bar_setting(10);
                if (callback.GetReturnValuetoJSON()[1].Count == 0)
                {
                    Debug.Log("-- 누적 결제 이벤트 정보 없음 --");
                    Insert_user_event_pay_info();
                }
                else
                {
                    Debug.Log("-- 누적 결제 이벤트 정보 가져오기 성공 --");
                    DBManager.Instance.DB_Insert_user_event_pay_info(callback.GetReturnValuetoJSON());
                    Get_user_event_playTime_info();
                }
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 누적 결제 이벤트 정보 가져오기 성공 --");
                server_loading = false;
                error_type = 33;
                Server_connect_check();
            }
        });

    }
    public void Get_user_event_playTime_info()
    {
        Debug.Log("----- Get_user_event_playTime_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(157);

        Backend.GameData.GetMyData(user_event_playTime_info, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                LoadingManager.Instance.loading_bar_setting(10);
                if (callback.GetReturnValuetoJSON()[1].Count == 0)
                {
                    Debug.Log("-- 플레이 타임 이벤트 정보 없음 --");
                    Insert_user_event_playTime_info();
                }
                else
                {
                    Debug.Log("-- 플레이 타임 이벤트 정보 가져오기 성공 --");
                    DBManager.Instance.DB_Insert_user_event_playTime_info(callback.GetReturnValuetoJSON());
                    Get_user_event_mission_info();
                }
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 플레이 타임 이벤트 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 33;
                Server_connect_check();
            }
        });
    }
    public void Get_user_event_mission_info()
    {
        Debug.Log("----- Get_user_event_mission_info IN -----");
        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(157);

        Backend.GameData.GetMyData(user_event_mission_info, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                LoadingManager.Instance.loading_bar_setting(10);
                if (callback.GetReturnValuetoJSON()[1].Count == 0)
                {
                    Debug.Log("-- 미션 이벤트 정보 없음 --");
                    Insert_user_event_mission_info();
                }
                else
                {
                    Debug.Log("-- 미션 이벤트 정보 가져오기 성공 --");
                    DBManager.Instance.DB_Insert_user_event_mission_info(callback.GetReturnValuetoJSON());
                    Get_user_event_1st_anniversary_info();
                }
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.Log("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.Log("-- 미션 이벤트 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 33;
                Server_connect_check();
            }
        });
    }
    public void Get_user_event_1st_anniversary_info()
    {
        Debug.LogWarning("----- Get_user_event_1st_anniversary IN -----");
        //Get_ranking_reward();

        LoadingManager.Instance.loading_text.text = DBManager.Instance.Language_converter(157);

        Backend.GameData.GetMyData(user_event_1st_anniversary_info, new Where(), 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                LoadingManager.Instance.loading_bar_setting(10);
                if (callback.GetReturnValuetoJSON()[1].Count == 0)
                {
                    Debug.LogWarning("----- 설 이벤트 정보 없음 -----");
                    Insert_user_event_1st_anniversary_info();
                }
                else
                {
                    Debug.LogWarning("----- 설 이벤트 정보 가져오기 성공 -----");
                    DBManager.Instance.DB_Insert_user_event_1st_anniversary_info(callback.GetReturnValuetoJSON());
                    Get_ranking_reward();
                }
            }
            else if (callback.GetStatusCode() == "404")
            {
                Debug.LogWarning("테이블 정보를 찾을수 없습니다");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("공개 테이블이 아닙니다.");
            }
            else if (callback.GetStatusCode() == "412")
            {
                Debug.Log("비활성화 된 테이블 입니다.");
            }
            else if (callback.GetStatusCode() == "400")
            {
                Debug.Log("정보량이 100이 넘습니다");
            }
            else
            {
                Debug.LogWarning("----- 설 이벤트 정보 가져오기 성공 -----");
                server_loading = false;
                error_type = 33;
                Server_connect_check();
            }
        });

    }
    public void Get_notice_list()
    {
        Debug.Log("----- Get_notice_list IN -----");
        if (DBManager.Instance.language_index == 0)
        {
            Debug.Log("-- 한국 --");
            Backend.BMember.UpdateCountryCode(CountryCode.SouthKorea, (callback) =>
            {
                if (callback.GetStatusCode() == "200")
                {
                }
            });
        }
        else
        {
            Debug.Log("-- 미국 --");
            Backend.BMember.UpdateCountryCode(CountryCode.UnitedStates, (callback) =>
            {
                if (callback.GetStatusCode() == "200")
                {
                }
            });
        }
        Backend.Notice.NoticeList((callback) =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 공지 가져오기 성공 --");
                //Debug.Log(callback.GetReturnValue().ToString());
                StartCoroutine("load_notice_image", (callback.GetReturnValuetoJSON()));

                Get_user_nickname();
            }
            else
            {
                Debug.Log("-- 공지 가져오기 실패 --");
                server_loading = false;
                error_type = 23;
                Server_connect_check();
            }
        });
    }
    public void Get_user_nickname()
    {
        Debug.Log("----- Get_user_nickname IN -----");
        Backend.BMember.GetUserInfo((callback) =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 유저 닉네임 가져오기 성공 --");
                DBManager.Instance.user_info.nickname = callback.GetReturnValuetoJSON()["row"]["nickname"].ToString();
                Check_push();
                Get_event();
            }
            else
            {
                Debug.Log("-- 유저 닉네임 가져오기 실패 --");
                server_loading = false;
                error_type = 40;
                Server_connect_check();
            }

        });
    }
    public void Get_event()
    {
        Debug.Log("----- Get_event IN -----");
        Backend.Event.EventList((callback) =>
        {
            Debug.Log("get_event===================" + callback.GetStatusCode());
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 이벤트 정보 가져오기 성공 --");
                event_check = true;
                if (callback.GetReturnValuetoJSON()[0].Count > 0)
                {
                    for (int i = 0; i < callback.GetReturnValuetoJSON()[0].Count; i++)
                    {
                        Debug.Log("이벤트 목록 ::::::::::::::" + callback.GetReturnValuetoJSON()[0][i]["title"]["S"].ToString());
                        if (callback.GetReturnValuetoJSON()[0][i]["title"]["S"].ToString() == "newbie") // 신규유저 출석체크
                        {
                            Debug.Log("-- 신규 유저 출석 체크 이벤트 --");
                            newbie_event_start_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["startDate"]["S"].ToString());
                            newbie_event_end_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["endDate"]["S"].ToString());
                            if (newbie_event_start_time <= server_time && newbie_event_end_time > server_time)
                            {
                                newbie_event_time = true;
                            }
                            else
                            {
                                newbie_event_time = false;
                            }
                        }
                        if (callback.GetReturnValuetoJSON()[0][i]["title"]["S"].ToString() == "event_discount") // 1+1 미트 지급
                        {
                            Debug.Log("-- 1+1 미트 이벤트 --");
                            meat_discount_start_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["startDate"]["S"].ToString());
                            meat_discount_end_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["endDate"]["S"].ToString());
                            if (meat_discount_start_time <= server_time && meat_discount_end_time > server_time)
                            {
                                meat_discount_time = true;
                            }
                            else
                            {
                                meat_discount_time = false;
                            }
                        }
                        if (callback.GetReturnValuetoJSON()[0][i]["title"]["S"].ToString() == "event_point_5_test") // 포인트 이벤트
                        {
                            Debug.Log("-- 포인트 이벤트 --");
                            event_point_start_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["startDate"]["S"].ToString());
                            event_point_end_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["endDate"]["S"].ToString());
                            if (event_point_start_time <= server_time && event_point_end_time > server_time)
                            {
                                event_point_time = true;
                            }
                            else
                            {
                                event_point_time = false;
                            }
                        }
                        if (callback.GetReturnValuetoJSON()[0][i]["title"]["S"].ToString() == "event_pay_0") // 누적 결제 이벤트
                        {
                            Debug.Log("-- 누적 결제 이벤트 --");
                            event_pay_start_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["startDate"]["S"].ToString());
                            event_pay_end_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["endDate"]["S"].ToString());
                            if (event_pay_start_time <= server_time && event_pay_end_time > server_time)
                            {
                                event_pay_time = true;
                            }
                            else
                            {
                                event_pay_time = false;
                            }
                        }
                        if (callback.GetReturnValuetoJSON()[0][i]["title"]["S"].ToString() == "event_time") // 플레이 타임 이벤트
                        {
                            Debug.Log("-- 플레이 타임 이벤트 --");
                            event_playTime_start_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["startDate"]["S"].ToString());
                            event_playTime_end_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["endDate"]["S"].ToString());
                            if (event_playTime_start_time <= server_time && event_playTime_end_time > server_time)
                            {
                                event_playTime_time = true;
                            }
                            else
                            {
                                event_playTime_time = false;
                            }
                        }
                        if (callback.GetReturnValuetoJSON()[0][i]["title"]["S"].ToString() == "event_mission_6_test") // 미션 이벤트
                        {
                            Debug.Log("-- 미션 이벤트 --");
                            event_mission_start_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["startDate"]["S"].ToString());
                            event_mission_end_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["endDate"]["S"].ToString());
                            if (event_mission_start_time <= server_time && event_mission_end_time > server_time)
                            {
                                event_mission_time = true;
                            }
                            else
                            {
                                event_mission_time = false;
                            }
                        }

                        if (callback.GetReturnValuetoJSON()[0][i]["title"]["S"].ToString() == "event_new_year") // 설 이벤트
                        {
                            Debug.LogWarning("-- 설 이벤트 --");
                            event_1st_anniversary_start_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["startDate"]["S"].ToString());
                            event_1st_anniversary_end_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["endDate"]["S"].ToString());
                            if (event_1st_anniversary_start_time <= server_time && event_1st_anniversary_end_time > server_time)
                            {
                                event_1st_anniversary_time = true;
                            }
                            else
                            {
                                event_1st_anniversary_time = false;
                            }
                        }
                        //블랙프라이데이
                        if (callback.GetReturnValuetoJSON()[0][i]["title"]["S"].ToString() == "event_blackfriday")
                        {
                            Debug.LogWarning("-- 블랙프라이데이 이벤트 --");
                            event_blackfriday_start_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["startDate"]["S"].ToString());
                            event_blackfriday_end_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["endDate"]["S"].ToString());
                            if (event_blackfriday_start_time <= server_time && event_blackfriday_end_time > server_time)
                            {
                                event_blackfriday_time = true;
                            }
                            else
                            {
                                event_blackfriday_time = false;
                            }
                        }
                        // 랭킹전
                        if (callback.GetReturnValuetoJSON()[0][i]["title"]["S"].ToString() == "ranking_match")
                        {
                            Debug.LogWarning("-- 랭킹전 시간 --");
                            ranking_match_start_time = DateTime.Parse(callback.GetReturnValuetoJSON()[0][i]["startDate"]["S"].ToString());
                            Debug.Log(ranking_match_start_time);
                            Debug.Log(ranking_match_start_time.AddDays(7));
                            if (ranking_match_start_time <= server_time)
                            {
                                ranking_match_time = true;
                            }
                            else
                            {
                                ranking_match_time = false;
                            }
                            Debug.LogWarning("ranking_match_time : " + ranking_match_time);
                        }

                    }
                }
                Get_achievement_info();
            }
            else
            {
                Debug.Log("-- 이벤트 정보 가져오기 실패 --");
                server_loading = false;
                error_type = 21;
                Server_connect_check();
            }
        });


    }
    IEnumerator load_notice_image(JsonData callback)
    {
        for (int i = 0; i < callback[0].Count; i++)
        {
            if (callback[0][i].ContainsKey("linkUrl")) // 링크가 있으면
            {

                DBManager.Instance.list_notice_url.Add(callback[0][i]["linkUrl"]["S"].ToString());
            }
            else
            {
                DBManager.Instance.list_notice_url.Add("");
            }
            if (callback[0][i]["isPublic"]["S"].ToString() == "y")
            {
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(callback[0][i]["content"]["S"].ToString());
                //Debug.Log(callback[0][i]["content"]["S"].ToString());
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    DBManager.Instance.list_notice_image.Add(((DownloadHandlerTexture)www.downloadHandler).texture);
                }
            }
        }

    }
    public void Check_attendance() // 유저 출석 확인
    {
        Debug.Log("----- Check_attendance IN -----");
        string today = DateTime.Parse(DBManager.Instance.user_info.attendance_time).ToString("MM/dd/yyyy");
        string target_day = DateTime.Parse(Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).ToString("MM/dd/yyyy");

        string toMonth = DateTime.Parse(Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).ToString("MM/yyyy");
        //Debug.Log("출석확인 시간" + Backend.Utils.GetServerTime().GetReturnValue());
        //DateTime target = DateTime.Parse(Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).AddHours(13).AddMinutes(55);
        //Debug.Log("출석확인 테스트 합니다" + target.ToString());
        //
        //string target_day = target.ToString("MM/dd/yyyy");

        if (newbie_event_time == true)
        {
            string newbie_today = DateTime.Parse(DBManager.Instance.user_newbie_attendance_info.newbie_attendance_time).ToString("MM/dd/yyyy");
            if (System.DateTime.Parse(newbie_today) <= System.DateTime.Parse(target_day))
            {
                DBManager.Instance.user_newbie_attendance_info.newbie_attendance_count++;
                DBManager.Instance.user_newbie_attendance_info.newbie_attendance_check = 1;
                DBManager.Instance.user_newbie_attendance_info.newbie_attendance_time = System.DateTime.Parse(target_day).AddDays(1).ToString("MM/dd/yyyy");
                DBManager.Instance.DB_Update_user_newbie_attendance_info();
            }
        }

        if (DBManager.Instance.user_attendance_package.nomal_attendance_package_time != "0")
        {
            string attedance_today = DateTime.Parse(DBManager.Instance.user_attendance_package.nomal_attendance_package_time).ToString("MM/dd/yyyy");
            if (System.DateTime.Parse(attedance_today) <= System.DateTime.Parse(target_day))
            {
                DBManager.Instance.user_attendance_package.nomal_attendance_package_count++;
                DBManager.Instance.user_attendance_package.nomal_attendance_package_check = 1;
                DBManager.Instance.user_attendance_package.nomal_attendance_package_time = System.DateTime.Parse(target_day).AddDays(1).ToString("MM/dd/yyyy");

                DBManager.Instance.DB_Update_user_attendance_package_info();
            }
        }
        if (DBManager.Instance.user_attendance_package.premium_attendance_package_time != "0")
        {
            string attedance_today = DateTime.Parse(DBManager.Instance.user_attendance_package.premium_attendance_package_time).ToString("MM/dd/yyyy");
            if (System.DateTime.Parse(attedance_today) <= System.DateTime.Parse(target_day))
            {
                DBManager.Instance.user_attendance_package.premium_attendance_package_count++;
                DBManager.Instance.user_attendance_package.premium_attendance_package_check = 1;
                DBManager.Instance.user_attendance_package.premium_attendance_package_time = System.DateTime.Parse(target_day).AddDays(1).ToString("MM/dd/yyyy");

                DBManager.Instance.DB_Update_user_attendance_package_info();
            }
        }

        // 간식 미션 체크
        if (event_mission_time == true)
        {
            // 간식 미션이 클리어 조건보다 작을 때, 간식 패키지 구매 및 사용중일 때 체크 
            if (DBManager.Instance.user_info.item_package_purchase == 1 && DBManager.Instance.user_event_mission_info.event_mission_value[7] < 10 && DBManager.Instance.user_event_mission_info.event_mission_value[8] < 10)
            {
                //Debug.LogError("간식 미션 체크");
                DBManager.Instance.user_event_mission_info.event_mission_value[7] = 10;
                DBManager.Instance.user_event_mission_info.event_mission_value[8] = 10;
                DBManager.Instance.user_mission_update_time = 20;
                DBManager.Instance.DB_Update_user_event_mission_info();
            }
        }

        //설 이벤트 출석체크
        /*
        if (event_1st_anniversary_time == true)
        {
            string event_1st_anniversary_today = DateTime.Parse(DBManager.Instance.user_event_1st_anniversary_info.anniversary_attendance_time).ToString("MM/dd/yyyy");
            if (System.DateTime.Parse(event_1st_anniversary_today) <= System.DateTime.Parse(target_day))
            {
                DBManager.Instance.user_event_1st_anniversary_info.anniversary_attendance_count++;
                DBManager.Instance.user_event_1st_anniversary_info.anniversary_attendance_check = 1;
                DBManager.Instance.user_event_1st_anniversary_info.anniversary_attendance_time = System.DateTime.Parse(target_day).AddDays(1).ToString("MM/dd/yyyy");
                DBManager.Instance.user_1st_anniversary_update_time = 20;
                DBManager.Instance.DB_Update_user_event_1st_anniversary_info();
            }
        }
        */

        //누적 결제 이벤트 달 마다 초기화 
        if (System.DateTime.Parse(DBManager.Instance.user_event_pay_info.event_reset_check) < System.DateTime.Parse(toMonth))
        {
            DBManager.Instance.user_event_pay_info.pay_point = 0;
            DBManager.Instance.user_event_pay_info.event_reset_check = toMonth;
            for (int i = 0; i < DBManager.Instance.user_event_pay_info.pay_reward_check.Count; i++)
            {
                DBManager.Instance.user_event_pay_info.pay_reward_check[i] = 0;
            }
            DBManager.Instance.DB_Update_user_event_pay_info();
        }
        //매일 초기화
        if (System.DateTime.Parse(today) <= System.DateTime.Parse(target_day))
        {
            DBManager.Instance.user_info.attendance_count++;
            if (DBManager.Instance.user_info.attendance_count > 30)
            {
                DBManager.Instance.Attendance_reset();
            }
            DBManager.Instance.user_info.attendance_time = System.DateTime.Parse(target_day).AddDays(1).ToString("MM/dd/yyyy");
            DBManager.Instance.user_info.notice_time = "0";
            DBManager.Instance.user_info.attendance_check = 1;
            DBManager.Instance.user_info.point_treasure_ticket = 10;

            for (int i = 0; i < DBManager.Instance.user_info.user_quest_reward.Count; i++)
            {
                DBManager.Instance.user_info.user_quest_reward[i] = 0;
                DBManager.Instance.user_info.user_quest_value[i] = 0;
            }

            for (int i = 0; i < DBManager.Instance.user_event_playTime_info.playTime_reward_check.Count; i++)
            {
                DBManager.Instance.user_event_playTime_info.playTime_reward_check[i] = "0";
            }
            DBManager.Instance.user_event_playTime_info.playTime = 0;
            DBManager.Instance.user_boss_info.ticket_count = 3;
            DBManager.Instance.user_info.user_quest_value[0] = 1; // 로그인
            DBManager.Instance.DB_Update_user_info();
            DBManager.Instance.DB_Update_user_newbie_attendance_info();
            DBManager.Instance.DB_Update_user_attendance_package_info();
            DBManager.Instance.DB_Update_user_event_playTime_info();
            DBManager.Instance.DB_Update_user_boss_info();


            for (int i = 0; i < DBManager.Instance.user_event_mission_info.event_mission_value.Count; i++)
            {
                DBManager.Instance.user_event_mission_info.event_mission_value[i] = 0;
                DBManager.Instance.user_event_mission_info.event_mission_check[i] = 0;
            }

            DBManager.Instance.user_mission_update_time = 20;
            DBManager.Instance.DB_Update_user_event_mission_info();

            DBManager.Instance.user_info.user_item_power_ad_count = 20;
            DBManager.Instance.user_info.user_item_speed_ad_count = 20;
            DBManager.Instance.user_info.user_rune_ad_count = 20;

        }
        else
        {
            //  Debug.Log("시간이 지나지 않았습니다.");
        }

        // 간식 미션 체크
        if (event_mission_time == true)
        {
            // 간식 미션이 클리어 조건보다 작을 때, 간식 패키지 구매 및 사용중일 때 체크 
            if (DBManager.Instance.user_info.item_package_purchase == 1 && DBManager.Instance.user_event_mission_info.event_mission_value[7] != 10 && DBManager.Instance.user_event_mission_info.event_mission_value[8] != 10)
            {
                Debug.LogError("간식 미션 체크");
                DBManager.Instance.user_event_mission_info.event_mission_value[7] = 10;
                DBManager.Instance.user_event_mission_info.event_mission_value[8] = 10;
                DBManager.Instance.user_mission_update_time = 20;
                DBManager.Instance.DB_Update_user_event_mission_info();
            }
        }

        check_user_attendance = true;
    }
    public void Package_time_check()
    {
        string time = server_time.ToString(); // 현재시간
        string target_time = DBManager.Instance.user_info.package_active_time;

        if (System.DateTime.Parse(time) >= System.DateTime.Parse(target_time))
        {
            DBManager.Instance.user_info.package_active_time = "0";
            DBManager.Instance.DB_Update_user_info();
        }
        else
        {
            // Debug.Log("시간이 지나지 않았습니다.");
        }
    }
    public void Item_time_check(int index)
    {
        if (DBManager.Instance.user_info.user_item_time[index] != "0")
        {
            string time = server_time.ToString(); // 현재시간
            string target_time = DBManager.Instance.user_info.user_item_time[index];

            if (System.DateTime.Parse(time) >= System.DateTime.Parse(target_time))
            {
                DBManager.Instance.user_info.user_item_time[index] = "0";
                DBManager.Instance.DB_Update_user_info();

                if (index == 2)
                {
                    UIManager.Instance.game_panel.Auto_again_panel_cencel_setting();
                    DBManager.Instance.user_info.auto_again_setting = 0;
                }
            }
            else
            {
                // Debug.Log("시간이 지나지 않았습니다.");
            }
        }
    }
    public void Auto_again_time_check()
    {
        string time = server_time.ToString();
        string target_time = DBManager.Instance.user_info.auto_again_time;

        if (DBManager.Instance.user_info.auto_again_time != "0")
        {
            if (System.DateTime.Parse(time) >= System.DateTime.Parse(target_time))
            {
                DBManager.Instance.user_info.auto_again_time = "0";
                DBManager.Instance.user_info.auto_again_active = 0;
                DBManager.Instance.DB_Update_user_info();
            }
            else
            {
                DBManager.Instance.user_info.auto_again_active = 1;
                //현재 내 환생 아이템 시간이 흐르지 않게 해줘야함
                //시간 흐르는 로직을 막을 수 없으니 계속 시간을 1초씩 늘려준다.

                //유저 아이템이 시간이 1초라도 남아잇으면
                if (DBManager.Instance.user_info.user_item_time[2] != "0")
                {
                    DateTime item2 = DateTime.Parse(DBManager.Instance.user_info.user_item_time[2]);
                    DBManager.Instance.user_info.user_item_time[2] = item2.AddSeconds(1).ToString("MM/dd/yyyy H:mm:ss");
                }

                //Debug.LogWarningFormat("auto_time_check {0}", DBManager.Instance.user_info.user_item_time[2]);
            }

        }
    }
    //간식패키지 시간체크
    public void Item_package_time_check()
    {
        string time = server_time.ToString();
        string target_time = DBManager.Instance.user_info.item_package_time;

        if (DBManager.Instance.user_info.item_package_time != "0")
        {
            if (System.DateTime.Parse(time) >= System.DateTime.Parse(target_time))
            {
                DBManager.Instance.user_info.item_package_time = "0";
                DBManager.Instance.user_info.item_package_purchase = 0;
                DBManager.Instance.DB_Update_user_info();
            }
            else
            {
                DBManager.Instance.user_info.item_package_purchase = 1;
                //현재 내 간식 아이템 시간이 흐르지 않게 해줘야함
                //시간 흐르는 로직을 막을 수 없으니 계속 시간을 1초씩 늘려준다.

                //유저 아이템이 시간이 1초라도 남아잇으면
                if (DBManager.Instance.user_info.user_item_time[0] != "0")
                {
                    DateTime item1 = DateTime.Parse(DBManager.Instance.user_info.user_item_time[0]);
                    DBManager.Instance.user_info.user_item_time[0] = item1.AddSeconds(1).ToString("MM/dd/yyyy H:mm:ss");
                }
                if (DBManager.Instance.user_info.user_item_time[1] != "0")
                {
                    DateTime item2 = DateTime.Parse(DBManager.Instance.user_info.user_item_time[1]);
                    DBManager.Instance.user_info.user_item_time[1] = item2.AddSeconds(1).ToString("MM/dd/yyyy H:mm:ss");
                }

                //Debug.LogWarningFormat("Item_time_check {0}, {1}", DBManager.Instance.user_info.user_item_time[0], DBManager.Instance.user_info.user_item_time[1]);
            }
        }
    }
    public void Item_ad_time_check(int index)
    {
        string time = server_time.ToString();
        string target_time = DBManager.Instance.user_info.user_item_ad_time[index];

        if (System.DateTime.Parse(time) >= System.DateTime.Parse(target_time))
        {
            DBManager.Instance.user_info.user_item_ad_time[index] = "0";
            DBManager.Instance.DB_Update_user_info();
        }
        else
        {
            // Debug.Log("시간이 지나지 않았습니다.");
        }
    }
    public void treasure_ad_time_check()
    {
        string time = server_time.ToString();
        string target_time = DBManager.Instance.user_info.tresure_box_reward_time;

        if (System.DateTime.Parse(time) >= System.DateTime.Parse(target_time))
        {
            DBManager.Instance.user_info.tresure_box_reward_time = "0";
            DBManager.Instance.DB_Update_user_info();
        }
        else
        {
            //Debug.Log("시간이 지나지 않았습니다.");
        }
    }
    public void rune_ad_time_check()
    {
        string time = server_time.ToString();
        string target_time = DBManager.Instance.user_rune_info.rune_ad_time;

        if (System.DateTime.Parse(time) >= System.DateTime.Parse(target_time))
        {
            DBManager.Instance.user_rune_info.rune_ad_time = "0";
            DBManager.Instance.DB_Update_user_rune_info();
        }
        else
        {
            //Debug.Log("시간이 지나지 않았습니다.");
        }
    }
    public void pvp_ticket_time_check()
    {
        string time = server_time.ToString();
        string target_time = DBManager.Instance.user_info.pvp_time;
        if (System.DateTime.Parse(time) >= System.DateTime.Parse(target_time))
        {
            DBManager.Instance.user_info.pvp_time = "0";
            DBManager.Instance.DB_Update_user_info();
        }
        else
        {
            //Debug.Log("시간이 지나지 않았습니다.");
        }
    }
    public void pvp_ad_time_check()
    {
        string time = server_time.ToString();
        string target_time = DBManager.Instance.user_info.pvp_ad_time;

        if (System.DateTime.Parse(time) >= System.DateTime.Parse(target_time))
        {
            DBManager.Instance.user_info.pvp_ad_time = "0";
            DBManager.Instance.DB_Update_user_info();
        }
        else
        {
            // Debug.Log("시간이 지나지 않았습니다.");
        }
    }

    /*
    // 랭킹전 티켓 구매 관련 시간 체크
    public void ranking_match_ticket_time_check()
    {
        string time = server_time.ToString();
        string target_time = DBManager.Instance.user_ranking_match_info.ranking_match_ticket_time;

        if (DateTime.Parse(time) >= DateTime.Parse(target_time))
        {
            DBManager.Instance.user_ranking_match_info.ranking_match_ticket_time = "0";
            DBManager.Instance.DB_Update_user_ranking_match_info();
        }
        else
        {
            //Debug.Log("시간이 지나지 않았습니다.");
        }
    }
    */



    public void rune_time_update()
    {
        if (DBManager.Instance.user_rune_info.rune_time == "0")
        {
            DBManager.Instance.user_rune_info.rune_time = ServerManager.Instance.server_time.ToString(); ;
        }
        DBManager.Instance.DB_Update_user_rune_info();
    }
    public void pvp_ad_time_update()
    {
        DBManager.Instance.user_info.pvp_ad_time = System.DateTime.Parse(Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).AddMinutes(15).ToString("MM/dd/yyyy H:mm:ss");
        DBManager.Instance.DB_Update_user_info();
    }
    public void treasure_ad_time_update()
    {
        DBManager.Instance.user_info.tresure_box_reward_time = System.DateTime.Parse(Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).AddMinutes(15).ToString("MM/dd/yyyy H:mm:ss");
        DBManager.Instance.DB_Update_user_info();
    }
    public void rune_ad_time_update()
    {
        DBManager.Instance.user_rune_info.rune_ad_time = System.DateTime.Parse(Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).AddMinutes(15).ToString("MM/dd/yyyy H:mm:ss");
        DBManager.Instance.DB_Update_user_rune_info();
    }
    public void treasure_ad_time_update(int index)
    {
        DBManager.Instance.user_info.user_item_ad_time[index] = System.DateTime.Parse(Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).AddMinutes(15).ToString("MM/dd/yyyy H:mm:ss");
        DBManager.Instance.DB_Update_user_info();
    }
    public void save_gold_update()
    {
        if (ServerManager.Instance.CheckDataTampering())
        {
            ServerManager.Instance.save_check_data_tampering_log("방치보상", "");
            return;
        }
        else
        {
            if (UIManager.Instance.analyze_panel.is_start_analyze)
            {
                UIManager.Instance.analyze_panel.gold_count += UIManager.Instance.save_gold_panel.reward_gold_value * 2;
                UIManager.Instance.analyze_panel.point_count += UIManager.Instance.save_gold_panel.reward_point_value * 2;

                if (UIManager.Instance.analyze_panel_object.activeSelf)
                {
                    UIManager.Instance.analyze_panel.Set_analyze_panel();
                }
            }

            DBManager.Instance.user_info.gold += UIManager.Instance.save_gold_panel.reward_gold_value * 2;
            DBManager.Instance.user_info.point += UIManager.Instance.save_gold_panel.reward_point_value * 2;
            if (DBManager.Instance.user_info.user_level < 40)
                GameManager.Instance.player_info.user_get_exp(int.Parse(UIManager.Instance.save_gold_panel.reward_exp_value.text) * 2);
            DBManager.Instance.user_info.save_gold_time = ServerManager.Instance.server_time.ToString();
            DBManager.Instance.DB_Update_user_info();
            Invoke("delay_execution", 0.1f);
        }
    }
    public void delay_execution()
    {
        UIManager.Instance.Save_gold_particle();
        UIManager.Instance.off_save_gold_panel();
    }
    public void Item_time_update(int index, int count)
    {
        if (index == 0)
        {
            if (DBManager.Instance.user_info.user_item_time[index] == "0")
            {
                DBManager.Instance.user_info.user_item_time[index] = System.DateTime.Parse(Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).AddMinutes(30 * count).ToString("MM/dd/yyyy H:mm:ss");
            }
            else
            {
                DBManager.Instance.user_info.user_item_time[index] = System.DateTime.Parse(DBManager.Instance.user_info.user_item_time[index]).AddMinutes(30 * count).ToString("MM/dd/yyyy H:mm:ss");
            }

        }
        else if (index == 1)
        {
            if (DBManager.Instance.user_info.user_item_time[index] == "0")
            {
                DBManager.Instance.user_info.user_item_time[index] = System.DateTime.Parse(Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).AddMinutes(30 * count).ToString("MM/dd/yyyy H:mm:ss");
            }
            else
            {
                DBManager.Instance.user_info.user_item_time[index] = System.DateTime.Parse(DBManager.Instance.user_info.user_item_time[index]).AddMinutes(30 * count).ToString("MM/dd/yyyy H:mm:ss");
            }
        }
        else if (index == 2)
        {
            if (DBManager.Instance.user_info.user_item_time[index] == "0")
            {
                DBManager.Instance.user_info.user_item_time[index] = System.DateTime.Parse(Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).AddHours(12 * count).ToString("MM/dd/yyyy H:mm:ss");
            }
            else
            {
                DBManager.Instance.user_info.user_item_time[index] = System.DateTime.Parse(DBManager.Instance.user_info.user_item_time[index]).AddHours(12 * count).ToString("MM/dd/yyyy H:mm:ss");
            }
        }


    }

    //네트워크 팝업 창 띄우기
    public void Server_connect_check()
    {
        if (SceneManager.GetActiveScene().name == "LoadingScene")
        {
            LoadingManager.Instance.network_popup_panel.gameObject.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (Is_backend_connect_status() == false || network_check == false)
            {
                //절전모드일 경우 절전모드 강제 해제
                if (GameManager.Instance.sleep_mode)
                {
                    GameManager.Instance.sleep_mode_end();
                }

                if (GameManager.Instance.boss_mode == true)
                {
                    UIManager.Instance.network_canvas_sub.gameObject.SetActive(true);
                }
                else if (GameManager.Instance.pvp_mode == true)
                {
                    UIManager.Instance.network_canvas_sub.gameObject.SetActive(true);
                }
                else if (GameManager.Instance.rune_mode == true)
                {
                    UIManager.Instance.network_canvas_sub.gameObject.SetActive(true);
                }
                else
                {
                    UIManager.Instance.network_canvas_main.gameObject.SetActive(true);
                }
            }
        }
    }
    public string Server_time_convert(JsonData json) // 현재 서버 시간 반환
    {
        string converter_server_time = "";
        converter_server_time = json["utcTime"].ToString(); // 현재    
        DateTime update_time = DateTime.Parse(converter_server_time);
        server_time = DateTime.Parse(converter_server_time);

        return update_time.ToString();
    }
    public void Server_time_update() // 서버 시간 업데이트
    {
        JsonData json = JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue());
        string converter_server_time = "";
        Debug.LogWarning("뒤끝 서버 시간 : " + json["utcTime"].ToString());
        converter_server_time = json["utcTime"].ToString(); // 현재   
        DateTime update_time = DateTime.Parse(converter_server_time);
        server_time = DateTime.Parse(converter_server_time);
        Debug.LogWarning("ServerTimeUpdate : " + server_time.ToString());
    }
    public void send_log_inapp(string product_id, string before_diamond, string after_diamond, string receipt)
    {
        Debug.Log("-send_log_inapp-");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("상품명", product_id);
        param.Add("결제전 다이아", before_diamond);
        param.Add("결제후 다이아", after_diamond);
        param.Add("영수증", receipt);


        Backend.GameLog.InsertLog("inapp", param, (callback) =>
        {
            // 이후 처리
        });
    }

    public void send_log_newbie_attendance(string day, string before_value, string after_value)
    {
        Debug.Log("-send_log_newbie_attendance-");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("일차", day);
        param.Add("받기전 보상", before_value);
        param.Add("받은후 보상", after_value);

        Backend.GameLog.InsertLog("newbie_attendance", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_year_attendance(string day, string before_value, string after_value)
    {
        Debug.Log("-send_log_year_attendance-");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("일차", day);
        param.Add("받기전 보상", before_value);
        param.Add("받은후 보상", after_value);

        Backend.GameLog.InsertLog("year_attendance", param, (callback) =>
        {
            // 이후 처리
        });
    }

    public void send_log_package(string product_id, string before_diamond, string after_diamond, string receipt, string before_item0, string after_item0
        , string before_item1, string after_item1, string before_item2, string after_item2, string before_unit, string after_unit)
    {
        Debug.Log("-send_log_package-");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("상품명", product_id);
        param.Add("결제전 다이아", before_diamond);
        param.Add("결제후 다이아", after_diamond);
        param.Add("결제전 아이템0", before_item0);
        param.Add("결제후 아이템0", after_item0);
        param.Add("결제전 아이템1", before_item1);
        param.Add("결제후 아이템1", after_item1);
        param.Add("결제전 아이템2", before_item2);
        param.Add("결제후 아이템2", after_item2);
        param.Add("결제전 유닛", before_unit);
        param.Add("결제후 유닛", after_unit);
        param.Add("영수증", receipt);
        Backend.GameLog.InsertLog("inapp", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_ad_package(string product_id, string receipt, string active, string active_time)
    {
        Debug.Log("send_log_ad_package in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("상품명", product_id);
        param.Add("영수증", receipt);
        param.Add("결제전 활성화", active);
        param.Add("결제 시간", active_time);
        Backend.GameLog.InsertLog("inapp", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_auto_package(string product_id, string receipt, string active, string active_time)
    {
        Debug.Log("send_log_auto_package in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("상품명", product_id);
        param.Add("영수증", receipt);
        param.Add("결제전 활성화", active);
        param.Add("결제 시간", active_time);
        Backend.GameLog.InsertLog("inapp", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_item_package(string product_id, string receipt, string active, string active_time)
    {
        Debug.Log("send_log_item_package in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("상품명", product_id);
        param.Add("영수증", receipt);
        param.Add("결제전 활성화", active);
        param.Add("결제 시간", active_time);
        Backend.GameLog.InsertLog("inapp", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_attendance_pacakge(string product_id, string receipt, string reward, string count, string reward_count, string check, string active_time)
    {
        Debug.Log("send_log_attendance_pacakge in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("상품명", product_id);
        param.Add("영수증", receipt);
        param.Add("결제후 출석보상", reward);
        param.Add("결제후 출석일", count);
        param.Add("결제후 출석보상 받은 횟수", reward_count);
        param.Add("결제후 출석체크", check);
        param.Add("결제후 출석날", active_time);

        Backend.GameLog.InsertLog("inapp", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_attendance_package_reward(string type, string before_reward, string after_reward, string before_count, string after_count, string before_reward_count, string after_reward_count, string before_check, string after_check, string active_time, string before_meat, string after_meat)
    {
        Debug.Log("send_log_attendance_package_reward in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));

        param.Add("타입", type);
        param.Add("결제전 출석보상", before_reward);
        param.Add("결제후 출석보상", after_reward);
        param.Add("결제전 출석일", before_count);
        param.Add("결제후 출석일", after_count);
        param.Add("결제전 출석보상 받은 횟수", before_reward_count);
        param.Add("결제후 출석보상 받은 횟수", after_reward_count);
        param.Add("결제전 출석체크", before_check);
        param.Add("결제후 출석체크", after_check);
        param.Add("결제전 출석날", active_time);
        param.Add("결제전 미트", before_meat);
        param.Add("결제후 미트", after_meat);

        Backend.GameLog.InsertLog("attendance_package", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_item(string before_diamond, string after_diamond, string before_item0, string after_item0, string before_item1, string after_item1, string before_item2, string after_item2)
    {
        Debug.Log("send_log_item in");
        Param param = new Param();
        param.Add("현재시간", DateTime.Now);
        param.Add("결제전 다이아", before_diamond);
        param.Add("결제후 다이아", after_diamond);
        param.Add("결제전 아이템0", before_item0);
        param.Add("결제후 아이템0", after_item0);
        param.Add("결제전 아이템1", before_item1);
        param.Add("결제후 아이템1", after_item1);
        param.Add("결제전 아이템2", before_item2);
        param.Add("결제후 아이템2", after_item2);

        Backend.GameLog.InsertLog("item", param, (callback) =>
        {
            // 이후 처리
        });
    }

    public void send_log_skill_reset(string before_diamond, string after_diamond, string before_skill_point, string after_skill_point, string before_skill, string after_skill)
    {
        Debug.Log("send_log_skill_reset in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("결제전 다이아", before_diamond);
        param.Add("결제후 다이아", after_diamond);
        param.Add("결제전 스킬포인트", before_skill_point);
        param.Add("결제후 스킬포인트", after_skill_point);
        param.Add("결제전 스킬", before_skill);
        param.Add("결제후 스킬", after_skill);


        Backend.GameLog.InsertLog("skill", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_treasure(string type, string before_user_unit, string after_user_unit, string unit_index, string before_price, string after_price)
    {
        Debug.Log("send_log_treasure in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("뽑기 종류", type);
        param.Add("뽑은 유닛", unit_index);
        param.Add("뽑기전 유닛", before_user_unit);
        param.Add("뽑은후 유닛", after_user_unit);
        param.Add("결제전 재화", before_price);
        param.Add("결제후 재화", after_price);

        Backend.GameLog.InsertLog("treasure", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_event_mission(string index, string before_diaond, string after_diamond, string before_mission_check, string after_mission_check)
    {
        Debug.Log("send_log_chat_report in");
        Param param = new Param();
        param.Add("현재시간", DateTime.Now);
        param.Add("닉네임", DBManager.Instance.user_info.nickname);
        param.Add("보상전 미션체크", before_mission_check);
        param.Add("보상후 미션체크", after_mission_check);
        param.Add("보상전 다이아몬드", before_diaond);
        param.Add("보상후 다이아몬드", after_diamond);

        Backend.GameLog.InsertLog("mission_event", param, (callback) =>
        {
            // 이후 처리
        });
    }

    public void send_log_exchange(string before_diaond, string after_diamond, string before_item, string after_item)
    {
        Debug.Log("send_log_chat_report in");
        Param param = new Param();
        param.Add("현재시간", DateTime.Now);
        param.Add("닉네임", DBManager.Instance.user_info.nickname);
        param.Add("교환전 아이템", before_item);
        param.Add("교환후 아이템", after_item);
        param.Add("교환전 다이아몬드", before_diaond);
        param.Add("교환후 다이아몬드", after_diamond);

        Backend.GameLog.InsertLog("item_exchange", param, (callback) =>
        {
            // 이후 처리
        });
    }

    public void send_log_use_item(string before_item, string after_item)
    {
        Param param = new Param();
        param.Add("현재시간", DateTime.Now);
        param.Add("닉네임", DBManager.Instance.user_info.nickname);
        param.Add("사용전 아이템", before_item);
        param.Add("사용후 아이템", after_item);

        Backend.GameLog.InsertLog("item_use", param, (callback) =>
        {
            // 이후 처리
        });
    }

    public void send_log_rune_upgrade(string count, string before_rune_level, string before_rune_stock, string after_rune_level, string after_rune_stock)
    {
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("닉네임", DBManager.Instance.user_info.nickname);
        param.Add("성공 / 실패 카운트", count);
        param.Add("강화 전 룬 레벨", before_rune_level);
        param.Add("강화 전 룬 갯수", before_rune_stock);
        param.Add("강화 후 룬 레벨", after_rune_level);
        param.Add("강화 후 룬 갯수", after_rune_stock);

        Backend.GameLog.InsertLog("rune_upgrade", param, (callback) =>
        {
            // 이후 처리
        });
    }

    public void send_log_cherry(string type, string before_value, string after_value, string before_point, string after_point)
    {
        Debug.Log("send_log_cherry in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("보상 종류", type);
        param.Add("보상전", before_value);
        param.Add("보상후", after_value);
        param.Add("보상전 포인트", before_point);
        param.Add("보상후 포인트", after_point);

        Backend.GameLog.InsertLog("cherry_point", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_1st_anniversary_event(string type, string before_value, string after_value)
    {
        Debug.LogWarning("send_log_1st_anniversary in");
        Param param = new Param();
        param.Add("현재시간", DateTime.Now);
        param.Add("닉네임", DBManager.Instance.user_info.nickname);
        param.Add("보상 종류", type);
        param.Add("보상전", before_value);
        param.Add("보상후", after_value);

        Backend.GameLog.InsertLog("1st_anniversary_event", param, (callback) =>
        {
            // 이후 처리
        });
    }

    public void send_log_data_loading_error(string type, string before_value, string after_value)
    {
        Debug.LogWarning("send_log_data_loading_error");
        Param param = new Param();
        param.Add("현재시간", DateTime.Now);
        param.Add("닉네임", DBManager.Instance.user_info.nickname);


        Backend.GameLog.InsertLog("data_loading_error", param, (callback) =>
        {
            // 이후 처리
        });
    }

    public void send_log_pet_treasure(string type, string before_user_pet, string after_user_pet, string pet_type, string pet_index, string before_price, string after_price)
    {
        Debug.Log("send_log_pet_treasure in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("뽑기 종류", type);
        param.Add("뽑은 펫 타입", pet_type);
        param.Add("뽑은 펫 번호", pet_index);
        param.Add("뽑기전 펫", before_user_pet);
        param.Add("뽑은후 펫", after_user_pet);
        param.Add("결제전 재화", before_price);
        param.Add("결제후 재화", after_price);

        Backend.GameLog.InsertLog("pet_treasure", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_rune(string type, string before_diamond, string before_rune, string after_diamond, string after_rune)
    {
        Debug.Log("send_log_rune in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("타입", type);
        param.Add("결제전 다이아", before_diamond);
        param.Add("결제후 다이아", after_diamond);
        param.Add("결제전 룬", before_rune);
        param.Add("결제후 룬", after_rune);

        Backend.GameLog.InsertLog("rune", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_rune_stage(string type, string before_rune, string after_rune, string before_rune_stage, string after_rune_stage, string ticket)
    {
        Debug.Log("send_log_rune_stage in");
        Debug.Log("servertime: " + server_time);
        Param param = new Param();
        param.Add("현재시간", server_time);
        param.Add("클리어", type);
        param.Add("클리어전 룬", before_rune);
        param.Add("클리어후 룬", after_rune);
        param.Add("클리어전 스테이지", before_rune_stage);
        param.Add("클리어후 스테이지", after_rune_stage);
        param.Add("룬 티켓", ticket);


        Backend.GameLog.InsertLog("rune_stage", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_attendance(int type, Int64 before_price, Int64 after_price)
    {
        Debug.Log("send_log_attendance in");
        Param param = new Param();
        param.Add("현재시간", server_time);
        param.Add("보상 종류", type);
        param.Add("보상전 재화", before_price);
        param.Add("보상후 재화", after_price);


        Backend.GameLog.InsertLog("attendance", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_achievement(int type, long before_price, long after_price)
    {
        Debug.Log("send_log_achievement in");
        Param param = new Param();
        param.Add("현재시간", server_time);
        param.Add("보상 종류", type);
        param.Add("보상전 재화", before_price);
        param.Add("보상후 재화", after_price);

        Backend.GameLog.InsertLog("achievement", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_again(long before_stage, long stage, long before_point, long after_point)
    {
        Debug.Log("send_log_again in");
        Param param = new Param();
        param.Add("현재시간", server_time);
        param.Add("환생층", before_stage);
        param.Add("환생후", stage);
        param.Add("환생전 포인트", before_point);
        param.Add("환생후 포인트", after_point);
        param.Add("자동 환생 패키지", DBManager.Instance.user_info.auto_again_active);
        param.Add("자동 환생 설정", DBManager.Instance.user_info.auto_again_setting);
        param.Add("자동 환생 설정층", DBManager.Instance.user_info.auto_again_stage);

        Backend.GameLog.InsertLog("again", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_quest(int type, long before_price, long after_price)
    {
        Debug.Log("send_log_quest in");
        Param param = new Param();
        param.Add("현재시간", server_time);
        param.Add("보상 종류", type);
        param.Add("보상전 재화", before_price);
        param.Add("보상후 재화", after_price);

        Backend.GameLog.InsertLog("quest", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_compose(string before_unit, string after_unit, string value, int compose_count, int grade)
    {
        Debug.Log("send_log_compose in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("합성전 유닛", before_unit);
        param.Add("합성후 유닛", after_unit);
        param.Add("합성 게이지", compose_count);
        param.Add("뽑은 유닛", value);
        param.Add("합성 등급", grade);

        Backend.GameLog.InsertLog("compose", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_pet_compose(string before_pet, string after_pet, string type, string index, string meterial)
    {
        Debug.Log("send_log_pet_compose in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("합성전 펫", before_pet);
        param.Add("합성후 펫", after_pet);
        param.Add("합성팻 타입", type);
        param.Add("합성펫 번호", index);
        param.Add("합성 재료" + meterial);

        Backend.GameLog.InsertLog("pet_compose", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_drone(int value, long before_value, long after_value)
    {
        Debug.Log("send_log_drone in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("받은 보상", value);
        param.Add("받기전 아이템", before_value);
        param.Add("받은뒤 아이템", after_value);

        Backend.GameLog.InsertLog("drone", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_boss(int boss_grade, int boss_index, long boss_damage, long boss_life, long gold, long point, long after_gold, long after_point)
    {
        Debug.Log("send_log_boss in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("보스등급", boss_grade);
        param.Add("보스번호", boss_index);
        param.Add("보스데미지", boss_damage);
        param.Add("보스남은체력", boss_life);
        param.Add("획득전골드", gold);
        param.Add("획득전포인트", point);
        param.Add("획득후골드", gold);
        param.Add("획득후포인트", point);

        Backend.GameLog.InsertLog("boss", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_boss_change(int before_boss_index, int after_boss_index, long before_meet, long after_meet)
    {
        Debug.Log("send_log_boss_change in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("변경전 보스", before_boss_index);
        param.Add("변경후 보스", after_boss_index);
        param.Add("변경전 미트", before_meet);
        param.Add("변경후 미트", after_meet);

        Backend.GameLog.InsertLog("boss_change", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_boss_reset(int boss_grade, int before_boss_index, int after_boss_index, long before_meet, long after_meet)
    {
        Debug.Log("send_log_boss_reset in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("변경 보스 등급", boss_grade);
        param.Add("변경전 보스", before_boss_index);
        param.Add("변경후 보스", after_boss_index);
        param.Add("변경전 미트", before_meet);
        param.Add("변경후 미트", after_meet);

        Backend.GameLog.InsertLog("boss_reset", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_boss_buy_ticket(int beroe_ticket, int after_ticket, long before_meet, long after_meet)
    {
        Debug.Log("send_log_boss_buy_ticket in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("받기전 티켓", beroe_ticket);
        param.Add("받은뒤 티켓", after_ticket);
        param.Add("변경전 미트", before_meet);
        param.Add("변경후 미트", after_meet);

        Backend.GameLog.InsertLog("boss_ticket", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_pvp_buy_ticket(int beroe_ticket, int after_ticket, long before_meet, long after_meet)
    {
        Debug.Log("send_log_pvp_buy_ticket in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("받기전 티켓", beroe_ticket);
        param.Add("받은뒤 티켓", after_ticket);
        param.Add("변경전 미트", before_meet);
        param.Add("변경후 미트", after_meet);

        Backend.GameLog.InsertLog("pvp_ticket", param, (callback) =>
        {
            // 이후 처리
        });
    }

    public void send_log_pvp(int beroe_ticket, int after_ticket, int before_point, int after_point)
    {
        Debug.Log("send_log_pvp in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("대결전 티켓", beroe_ticket);
        param.Add("대결후 티켓", after_ticket);
        param.Add("대결전 점수", before_point);
        param.Add("대결후 점수", after_point);

        Backend.GameLog.InsertLog("pvp", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_server_error(int error_type, string status_code, string message, string text)
    {

        Debug.Log("send_log_server_error in");
        Param param = new Param();

        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("에러 타입", error_type);
        param.Add("에러코드", status_code);
        param.Add("에러메세지", message);
        param.Add("내용", text);

        Backend.GameLog.InsertLog("error", param, (callback) =>
        {
            // 이후 처리
        });

    }
    public void send_log_server_error(int error_type, string status_code, string message)
    {

        Debug.Log("send_log_server_error in");
        Debug.LogError(error_type);
        Debug.LogError(status_code);
        Debug.LogError(message);
        Param param = new Param();

        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("에러 타입", error_type);
        param.Add("에러코드", status_code);
        param.Add("에러메세지", message);

        Backend.GameLog.InsertLog("error", param, (callback) =>
        {
            // 이후 처리
        });

    }
    public void send_log_cheat()
    {
        Debug.Log("send_log_cheat in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));


        Backend.GameLog.InsertLog("cheat", param, (callback) =>
        {
            Application.Quit();
        });
    }
    public void send_log_chat_report(string nickname, string msg)
    {
        Debug.Log("send_log_chat_report in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("닉네임", nickname);
        param.Add("메세지", msg);

        Backend.GameLog.InsertLog("report", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void send_log_play_event_reward(string before_reward, string after_reward, string before_value, string after_value)
    {
        Debug.Log("send_log_chat_report in");
        Param param = new Param();
        param.Add("현재시간", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("닉네임", DBManager.Instance.user_info.nickname);
        param.Add("보상전 리워드", before_reward);
        param.Add("보상후 리워드", after_reward);
        param.Add("플레이 타임", DBManager.Instance.user_event_playTime_info.playTime.ToString());
        param.Add("보상전 값", before_value);
        param.Add("보상후 값", after_value);

        Backend.GameLog.InsertLog("play_time_event", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void sned_log_login(string info)
    {
        Debug.Log("sned_log_login in");
        Param param = new Param();
        param.Add("로그인 정보", info);
        Backend.GameLog.InsertLog("login", param, (callback) =>
        {
            // 이후 처리
        });
    }
    public void Get_ranking_user_info(int rank, string type)
    {
        Debug.Log("Get_ranking_user_info in");
        Where where = new Where();
        if (type == "power")
        {
            where.Equal("owner_inDate", DBManager.Instance.list_power[rank].InDate);
        }
        else if (type == "total")
        {
            where.Equal("owner_inDate", DBManager.Instance.list_total_stage[rank].InDate);
        }
        else if (type == "best")
        {
            where.Equal("owner_inDate", DBManager.Instance.list_best_stage[rank].InDate);
        }
        else if (type == "ranking_match")
        {
            where.Equal("owner_inDate", DBManager.Instance.list_ranking_match[rank].InDate);
        }
        Backend.GameData.Get("user_pvp", where, (callback) =>
        {
            if (callback.GetStatusCode() == "200")
            {
                UIManager.Instance.ranking_panel.Ranking_info_panel_setting(callback.GetReturnValuetoJSON());
            }
            else
            {

            }
        });
    }

    public void Get_power_ranking()
    {
        Debug.Log("Get_power_ranking in");
        Backend.RTRank.GetRTRankByUuid(power_ranking, 100, callback1 =>
        {
            //Debug.Log("#################토탈 파워 상위 받아오기" + callback1.GetStatusCode());
            if (callback1.GetStatusCode() == "200")
            {
                DBManager.Instance.DB_Insert_user_ranking(callback1.GetReturnValuetoJSON());
                Backend.RTRank.GetMyRTRank(power_ranking, 0, callback4 =>
                {
                    //Debug.Log("#################내 파워 랭킹 받아오기" + callback4.GetStatusCode());
                    if (callback4.GetStatusCode() == "200")
                    {
                        //Debug.Log(callback1.GetReturnValue());
                        DBManager.Instance.ckeck_current_rank = true;
                        DBManager.Instance.DB_inster_user_rank(callback4.GetReturnValuetoJSON(), "power");
                        UIManager.Instance.pvp_panel.current_power_rank = int.Parse(callback4.GetReturnValuetoJSON()["rows"][0]["rank"]["N"].ToString());
                        if (UIManager.Instance.ranking_panel.currentRankState == 1)
                        {
                            UIManager.Instance.ranking_panel.Ranking_panel_power_update();
                        }
                        server_loading = false;
                    }
                    else
                    {
                        DBManager.Instance.ckeck_current_rank = false;
                        server_loading = false;
                        error_type = 11;
                        Server_connect_check();
                        send_log_server_error(error_type, callback4.GetStatusCode().ToString(), callback4.GetMessage().ToString());
                    }
                });
            }
            else
            {
                DBManager.Instance.ckeck_current_rank = false;
                server_loading = false;
                error_type = 11;
                Server_connect_check();
                send_log_server_error(error_type, callback1.GetStatusCode().ToString(), callback1.GetMessage().ToString());
            }
        });
    }

    public void Get_power_ranking_sync()
    {
        Debug.Log("Get_power_ranking in");
        var getRTRankByUuid = Backend.RTRank.GetRTRankByUuid(power_ranking, 100);
        if (getRTRankByUuid.IsSuccess())
        {
            //Debug.Log("#################토탈 파워 상위 받아오기" + callback1.GetStatusCode());
            if (getRTRankByUuid.GetStatusCode() == "200")
            {
                DBManager.Instance.DB_Insert_user_ranking(getRTRankByUuid.GetReturnValuetoJSON());
                var getMyRTRank = Backend.RTRank.GetMyRTRank(power_ranking, 0);
                if (getMyRTRank.IsSuccess())
                {
                    //Debug.Log("#################내 파워 랭킹 받아오기" + callback4.GetStatusCode());
                    if (getMyRTRank.GetStatusCode() == "200")
                    {
                        //Debug.Log(callback1.GetReturnValue());
                        DBManager.Instance.DB_inster_user_rank(getMyRTRank.GetReturnValuetoJSON(), "power");
                        UIManager.Instance.pvp_panel.current_power_rank = int.Parse(getMyRTRank.GetReturnValuetoJSON()["rows"][0]["rank"]["N"].ToString());
                        if (UIManager.Instance.ranking_panel.currentRankState == 1)
                        {
                            UIManager.Instance.ranking_panel.Ranking_panel_power_update();
                            UIManager.Instance.ranking_panel.currentRankState = 0;
                        }
                        server_loading = false;

                    }
                    else
                    {
                        server_loading = false;
                        error_type = 11;
                        Server_connect_check();
                        send_log_server_error(error_type, getMyRTRank.GetStatusCode().ToString(), getMyRTRank.GetMessage().ToString());
                    }

                }
            }
            else
            {
                server_loading = false;
                error_type = 11;
                Server_connect_check();
                send_log_server_error(error_type, getRTRankByUuid.GetStatusCode().ToString(), getRTRankByUuid.GetMessage().ToString());
            }
        }
    }


    public void Get_total_stage_ranking()
    {
        Debug.Log("Get_total_stage_ranking in");
        Backend.RTRank.GetRTRankByUuid(total_stage_ranking, 100, callback3 =>
        {
            //  Debug.Log("#################파워랭킹 상위 받아오기" + callback3.GetStatusCode());
            if (callback3.GetStatusCode() == "200")
            {
                DBManager.Instance.DB_Insert_user_ranking(callback3.GetReturnValuetoJSON());

                Backend.RTRank.GetMyRTRank(total_stage_ranking, 0, callback5 =>
                {
                    // Debug.Log("#################내 토탈 스테이지 랭킹 받아오기" + callback5.GetStatusCode());
                    if (callback5.GetStatusCode() == "200")
                    {
                        DBManager.Instance.DB_inster_user_rank(callback5.GetReturnValuetoJSON(), "total_stage");
                        UIManager.Instance.ranking_panel.Ranking_panel_total_stage_update();
                        server_loading = false;
                    }
                    else
                    {
                        server_loading = false;
                        error_type = 12;
                        Server_connect_check();
                        send_log_server_error(error_type, callback5.GetStatusCode().ToString(), callback5.GetMessage().ToString());
                    }

                });
            }
            else
            {
                server_loading = false;
                error_type = 12;
                Server_connect_check();
                send_log_server_error(error_type, callback3.GetStatusCode().ToString(), callback3.GetMessage().ToString());
            }
        });

    }
    public void Get_best_stage_ranking()
    {
        Debug.Log("Get_best_stage_ranking in");
        Backend.RTRank.GetRTRankByUuid(best_stage_ranking, 100, callback2 =>
        {
            //  Debug.Log("#################최고 스테이지 상위 받아오기" + callback2.GetStatusCode());
            if (callback2.GetStatusCode() == "200")
            {
                DBManager.Instance.DB_Insert_user_ranking(callback2.GetReturnValuetoJSON());
                Backend.RTRank.GetMyRTRank(best_stage_ranking, 0, callback6 =>
                {
                    //   Debug.Log("#################내 최고 스테이지 랭킹 받아오기" + callback6.GetStatusCode());
                    if (callback6.GetStatusCode() == "200")
                    {
                        DBManager.Instance.DB_inster_user_rank(callback6.GetReturnValuetoJSON(), "best_stage");

                        UIManager.Instance.ranking_panel.Ranking_panel_best_stage_update();

                        server_loading = false;
                    }
                    else
                    {
                        server_loading = false;
                        error_type = 13;
                        Server_connect_check();
                        send_log_server_error(error_type, callback6.GetStatusCode().ToString(), callback6.GetMessage().ToString());
                    }

                });
            }
            else
            {
                server_loading = false;
                error_type = 13;
                Server_connect_check();
                send_log_server_error(error_type, callback2.GetStatusCode().ToString(), callback2.GetMessage().ToString());
            }
        });

    }
    public void Get_user_pvp_ranking()
    {
        Debug.Log("Get_user_pvp_ranking in");
        server_loading_time = 0;
        server_loading = true;

        error_type = 9;
        Backend.RTRank.GetMyRTRank(pvp_ranking, 0, callback1 =>
        {

            //Debug.Log("@@@@@@@@@@@@@@@@대련 테스트" + callback1.GetReturnValue());
            if (callback1.GetStatusCode() == "200")
            {
                UIManager.Instance.pvp_panel.pvp_ranking_ready_text.gameObject.SetActive(false);
                UIManager.Instance.pvp_game_panel.pvp_result_rank_after_text.text = callback1.GetReturnValuetoJSON()["rows"][0]["rank"]["N"].ToString();

                DBManager.Instance.DB_inster_user_rank(callback1.GetReturnValuetoJSON(), "pvp_point");
                Backend.RTRank.GetRTRankByUuid(pvp_ranking, 100, callback2 =>
                {
                    //  Debug.Log("@@@@@@@@@@@@@@@@상위 랭킹 받아오기" + callback2.GetStatusCode());
                    if (callback2.GetStatusCode() == "200")
                    {
                        DBManager.Instance.DB_Insert_user_ranking(callback2.GetReturnValuetoJSON());

                        server_loading = false;
                        UIManager.Instance.pvp_panel.pvp_ranking_panel_setting();

                        UIManager.Instance.pvp_panel.pvp_ranking_ready_text.gameObject.SetActive(false);
                    }
                    else if (callback2.GetReturnValue() == "428")
                    {
                        server_loading = false;
                        UIManager.Instance.pvp_panel.pvp_ranking_ready_text.gameObject.SetActive(true);
                    }
                    else
                    {
                        UIManager.Instance.pvp_panel.pvp_ranking_ready_text.gameObject.SetActive(false);
                        server_loading = false;
                        error_type = 9;
                        Server_connect_check();
                        send_log_server_error(error_type, callback2.GetStatusCode().ToString(), callback2.GetMessage().ToString());
                    }
                });
            }
            else if (callback1.GetStatusCode() == "404")
            {
                server_loading = false;
                UIManager.Instance.pvp_panel.pvp_ranking_ready_text.gameObject.SetActive(true);
                Debug.Log("집계중입니다.");
            }
            else
            {
                UIManager.Instance.pvp_panel.pvp_ranking_ready_text.gameObject.SetActive(false);
                server_loading = false;
                error_type = 9;
                Server_connect_check();
                send_log_server_error(error_type, callback1.GetStatusCode().ToString(), callback1.GetMessage().ToString());
            }
        });

    }
    public void Get_pvp_end_time()
    {
        Debug.Log("Get_pvp_end_time in");
        Backend.RTRank.GetRTRankByUuid(pvp_ranking, 1, callback2 =>
        {
            //  Debug.Log("@@@@@@@@@@@@@@@@상위 랭킹 받아오기" + callback2.GetStatusCode());
            if (callback2.GetStatusCode() == "200")
            {
                DBManager.Instance.DB_Insert_user_ranking(callback2.GetReturnValuetoJSON());
            }
        });
    }
    public void Get_user_pvp_info()
    {
        Debug.Log("Get_user_pvp_info in");
        server_loading_time = 0;
        server_loading = true;

        error_type = 10;
        Backend.RTRank.GetMyRTRank(pvp_ranking, 0, callback1 =>
        {
            if (callback1.GetStatusCode() == "404") // 랭킹에 없음
            {
                DBManager.Instance.user_info.pvp_point = 1000;
                DBManager.Instance.DB_Update_user_info();
                Param param = new Param();
                param.Add("pvp_point", (long)DBManager.Instance.user_info.pvp_point);

                Backend.GameInfo.UpdateRTRankTable("user_info", "pvp_point", DBManager.Instance.user_info.inDate, (long)DBManager.Instance.user_info.pvp_point, callback =>
                {
                    if (callback.GetStatusCode() == "200")
                    {
                        Debug.LogWarning("@@@@@@@@@@@@@@@@랭킹 업데이트 완료");
                        Backend.RTRank.GetMyRTRank(pvp_point, 5, callback3 =>
                        {
                            //Debug.Log("@@@@@@@@@@@@@@@@내주위 랭킹 받아오기" + callback3.GetStatusCode());
                            if (callback3.GetStatusCode() == "200")
                            {
                                //Debug.Log("@@@@@@@@@@@@@@@@내주위 랭킹 받아오기 성공");
                                Check_enemy(callback3.GetReturnValuetoJSON());
                            }
                            else
                            {
                                server_loading = false;
                                error_type = 10;
                                Server_connect_check();
                                send_log_server_error(error_type, callback3.GetStatusCode().ToString(), callback3.GetMessage().ToString());
                            }
                        });
                    }
                    else if (callback.GetStatusCode() == "428") // 집계 시간
                    {
                        Backend.RTRank.GetMyRTRank(pvp_point, 5, callback3 =>
                        {
                            //Debug.Log("@@@@@@@@@@@@@@@@내주위 랭킹 받아오기" + callback3.GetStatusCode());
                            if (callback3.GetStatusCode() == "200")
                            {
                                Check_enemy(callback3.GetReturnValuetoJSON());
                                server_loading = false;
                            }
                            else
                            {
                                server_loading = false;
                                error_type = 10;
                                send_log_server_error(error_type, callback3.GetStatusCode().ToString(), callback3.GetMessage().ToString());
                            }
                        });
                        //server_loading = false;
                    }
                    else
                    {
                        server_loading = false;
                        error_type = 10;
                        Server_connect_check();
                        send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
                    }
                });
            }
            else if (callback1.GetStatusCode() == "200") // 랭킹에 있음
            {
                Backend.GameInfo.UpdateRTRankTable("user_info", "pvp_point", DBManager.Instance.user_info.inDate, (long)DBManager.Instance.user_info.pvp_point, callback =>
                {
                    if (callback.GetStatusCode() == "200") // 
                    {
                        Debug.LogWarning("@@@@@@@@@@@@@@@@랭킹 업데이트 완료2");
                        Backend.RTRank.GetMyRTRank(pvp_point, 5, callback3 =>
                        {
                            //Debug.Log("@@@@@@@@@@@@@@@@내주위 랭킹 받아오기" + callback3.GetStatusCode());
                            if (callback3.GetStatusCode() == "200")
                            {
                                Check_enemy(callback3.GetReturnValuetoJSON());
                                server_loading = false;
                            }
                            else
                            {
                                server_loading = false;
                                error_type = 10;
                                send_log_server_error(error_type, callback3.GetStatusCode().ToString(), callback3.GetMessage().ToString());
                            }
                        });
                    }
                    else if (callback.GetStatusCode() == "428") //집계시간임
                    {
                        Backend.RTRank.GetMyRTRank(pvp_point, 5, callback3 =>
                        {
                            if (callback3.GetStatusCode() == "200")
                            {
                                Check_enemy(callback3.GetReturnValuetoJSON());
                                server_loading = false;
                            }
                            else
                            {
                                server_loading = false;
                                error_type = 10;
                                send_log_server_error(error_type, callback3.GetStatusCode().ToString(), callback3.GetMessage().ToString());
                            }
                        });
                        //server_loading = false;
                    }
                    else
                    {
                        server_loading = false;
                        error_type = 10;
                        Server_connect_check();
                        send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
                    }
                });
            }
            else
            {
                server_loading = false;
                error_type = 10;
                Server_connect_check();
                send_log_server_error(error_type, callback1.GetStatusCode().ToString(), callback1.GetMessage().ToString());
                Debug.Log("랭킹 업데이트 실패");
            }
        });
    }
    public void Product_server_check(string product)
    {
        Debug.Log("Product_server_check in");
        //Debug.Log("Product_server_check");
        Backend.BMember.IsAccessTokenAlive((callback) =>
        {
            //Debug.Log("Product_server_check11");
            if (callback.GetStatusCode() == "204")
            {
                GameManager.Instance.Buy_product_clear(product);
            }
            else
            {
                ServerManager.Instance.product = product;
                error_type = 36;
                Server_connect_check();
            }
        });
    }
    public void CheckIsAccessTokenAlive()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            Backend.BMember.IsAccessTokenAlive((callback) =>
            {
                if (callback.GetStatusCode() == "204")
                {

                }
                else
                {
                    reflash_token();
                }
            });
        }
    }
    public void reflash_token()
    {
        Debug.Log("reflash_token in");
        Backend.BMember.RefreshTheBackendToken((callback) =>
        {
            Debug.Log("reflash_token" + callback.GetStatusCode());
            if (callback.GetStatusCode() == "201")
            {
                Debug.Log("토큰 리플래쉬 확인");
            }
            else
            {
                login_token();
            }
        });
    }
    public void login_token()
    {
        Debug.Log("login_token in");
        Backend.BMember.LoginWithTheBackendToken((callback) =>
        {
            Debug.Log("login_token" + callback.GetStatusCode());
            if (callback.GetStatusCode() == "201")
            {
                Debug.Log("토큰 재로그인");
            }
            else
            {
                Re_login();
            }
        });
    }
    public void Get_pvp_result()
    {
        Debug.Log("Get_pvp_result in");
        server_loading_time = 0;
        server_loading = true;

        int rank_temp = 0;

        Backend.GameInfo.UpdateRTRankTable("user_info", "pvp_point", DBManager.Instance.user_info.inDate, (long)DBManager.Instance.user_info.pvp_point, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                //  Debug.Log("랭킹 업데이트 완료");

                Backend.RTRank.GetMyRTRank(pvp_ranking, 0, callback1 =>
                {
                    //   Debug.Log("대련 테스트" + callback1.GetStatusCode());
                    if (callback1.GetStatusCode() == "200")
                    {

                        //Debug.Log(callback1.GetReturnValue());
                        JsonData rank = callback1.GetReturnValuetoJSON();
                        UIManager.Instance.pvp_panel.after_user_pvp_rank = int.Parse(rank[0][0]["rank"]["N"].ToString());
                        UIManager.Instance.pvp_panel.after_user_pvp_point = DBManager.Instance.user_info.pvp_point;
                        UIManager.Instance.pvp_panel.On_pvp_result_setting();
                        server_loading = false;
                    }
                });
            }
            else
            {
                Server_connect_check();
                send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
            }
        });
    }
    public void pvp_ranking_check()
    {
        Debug.Log("pvp_ranking_check in");
        Backend.RTRank.GetMyRTRank(pvp_ranking, 0, callback1 =>
        {

            if (callback1.GetStatusCode() == "404") // 랭킹에 없음
            {
                DBManager.Instance.user_info.pvp_point = 1000;
                DBManager.Instance.DB_Update_user_info();
            }
        });
    }
    public void Get_ranking_reward()
    {
        Debug.Log("----- Get_ranking_reward IN -----");
        DBManager.Instance.list_post_info.Clear();
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            server_loading_time = 0;
            server_loading = true;
        }
        Backend.Rank.GetRankRewardList((callback) =>
        {
            // Debug.Log("dddddddddddddddddddddddddddd" + callback.GetStatusCode().ToString());
            if (callback.GetStatusCode() == "200")
            {
                //   Debug.Log("dddddddddddddddddddddddddddd" + callback.GetReturnValue().ToString());
                Debug.Log("-- 랭킹 가져오기 성공 --");
                DBManager.Instance.DB_Insert_ranking_post_info(callback.GetReturnValuetoJSON());

                Get_post();
            }
            else
            {
                Debug.Log("-- 랭킹 가져오기 실패 --");
                Server_connect_check();
                send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
            }
        });
    }

    public void Get_post()
    {
        Debug.Log("----- Get_post IN -----");
        Backend.Social.Post.GetPostListV2(callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                Debug.Log("-- 우편 정보 가져오기 성공 --");
                //  Debug.Log("dddddddddddddddddddddddddddd" + callback.GetReturnValue().ToString());
                DBManager.Instance.DB_Insert_admin_post_info(callback.GetReturnValuetoJSON());
                Check_attendance();
            }
            else
            {
                Debug.Log("-- 우편 정보 가져오기 실패 --");
                Server_connect_check();
                send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
            }
        });
    }

    public void Send_ranking_reward(string indate)
    {
        Debug.Log("Send_ranking_reward in");
        Backend.Rank.ReceiveRankReward(indate, (callback) =>
        {
            server_loading_time = 0;
            server_loading = true;

            if (callback.GetStatusCode() == "200") // 수령 완료
            {
                DBManager.Instance.Send_post_reward(callback.GetReturnValuetoJSON());
                Get_ranking_reward();
            }
            else if (callback.GetStatusCode() == "404") // 존재하지 않음
            {
                server_loading = false;
                Debug.Log("우편이 존재 하지 않습니다.");
            }
            else
            {
                Server_connect_check();
                send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
            }

        });
    }
    public void Send_post(string indate)
    {
        Debug.Log("Send_post in");
        Backend.Social.Post.ReceiveAdminPostItemV2(indate, callback =>
        {
            server_loading_time = 0;
            server_loading = true;

            if (callback.GetStatusCode() == "200") // 수령 완료
            {
                DBManager.Instance.Send_post_reward(callback.GetReturnValuetoJSON());

                Get_ranking_reward();

            }
            else if (callback.GetStatusCode() == "404") // 존재하지 않음
            {
                server_loading = false;
                Debug.Log("우편이 존재 하지 않습니다.");
            }
            else
            {
                Server_connect_check();
                send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
            }

        });
    }
    public void Check_enemy(JsonData json)
    {
        Debug.LogWarning("Check_enemy in");
        // Debug.Log("Check_enemy -----" + json.ToJson());
        int random = 0;
        while (true)
        {
            if (json[0].Count > 1)
            {
                random = UnityEngine.Random.Range(0, json[0].Count);
                if (json[0][random]["nickname"].ToString() != DBManager.Instance.user_info.nickname.ToString())
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        Where where = new Where();
        where.Equal("owner_inDate", json[0][random]["gamerInDate"].ToString());
        Backend.GameData.Get("user_pvp", where, (callback) =>
        {
            if (callback.GetStatusCode() == "200")
            {
                DBManager.Instance.DB_insert_pvp_enemy(callback.GetReturnValuetoJSON(), json[0][random]["nickname"].ToString(), json[0][random]["score"]["N"].ToString());
                server_loading = false;
            }
            else
            {
                server_loading = false;
                error_type = 10;
                Server_connect_check();
                send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
            }
        });
        server_loading = false;
        //DBManager.Instance.DB_insert_pvp_enemy(get_enemy_info.GetReturnValuetoJSON(), json[0][random]["rank"]["N"].ToString(), json[0][random]["nickname"].ToString(), json[0][random]["score"]["N"].ToString());
    }

    public void Update_power_ranking()
    {
        Debug.Log("Update_power_ranking in");

        server_loading_time = 0;
        server_loading = true;

        error_type = 11;

        if (gm)
        {
            Backend.GameInfo.UpdateRTRankTable("user_info", "power", DBManager.Instance.user_info.inDate, 0, (callback) =>
            {
                Debug.Log("power =================" + callback.GetStatusCode());
                if (callback.GetStatusCode() == "200")
                {
                    Get_power_ranking();
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                }
                else if (callback.GetStatusCode() == "428")
                {
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(true);
                    Debug.Log("집계중입니다");
                    server_loading = false;
                }
                else
                {
                    Debug.LogError("전투력 업데이트 실패 GM");
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                    server_loading = false;
                    error_type = 11;
                    Server_connect_check();
                    send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
                }
            });
        }
        else
        {
            Backend.GameInfo.UpdateRTRankTable("user_info", "power", DBManager.Instance.user_info.inDate, DBManager.Instance.obscurelong_converter(DBManager.Instance.user_info.power), (callback) =>
            {
                Debug.Log("power =================" + callback.GetStatusCode());
                if (callback.GetStatusCode() == "200")
                {
                    Get_power_ranking();
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                }
                else if (callback.GetStatusCode() == "428")
                {
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(true);
                    Debug.Log("집계중입니다");
                    server_loading = false;
                }
                else
                {
                    Debug.LogError("전투력 업데이트 실패");
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                    server_loading = false;
                    error_type = 11;
                    Server_connect_check();
                    send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
                }
            });
        }
    }

    public void Update_power_ranking_sync()
    {
        Debug.Log("Update_power_ranking sync in");

        server_loading_time = 0;
        server_loading = true;

        error_type = 11;

        if (gm)
        {
            var gmUpdateRTRankTable = Backend.GameInfo.UpdateRTRankTable("user_info", "power", DBManager.Instance.user_info.inDate, 0);
            if (gmUpdateRTRankTable.IsSuccess())
            {
                Debug.Log("power =================" + gmUpdateRTRankTable.GetStatusCode());
                if (gmUpdateRTRankTable.GetStatusCode() == "200")
                {
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                }
                else if (gmUpdateRTRankTable.GetStatusCode() == "428")
                {
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(true);
                    Debug.Log("집계중입니다");
                    server_loading = false;
                }
                else
                {
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                    server_loading = false;
                    error_type = 11;
                    Server_connect_check();
                    send_log_server_error(error_type, gmUpdateRTRankTable.GetStatusCode().ToString(), gmUpdateRTRankTable.GetMessage().ToString());
                }
            }
        }
        else
        {
            var updateRTRankTable = Backend.GameInfo.UpdateRTRankTable("user_info", "power", DBManager.Instance.user_info.inDate, DBManager.Instance.obscurelong_converter(DBManager.Instance.user_info.power));
            if (updateRTRankTable.IsSuccess())
            {
                Debug.Log("power =================" + updateRTRankTable.GetStatusCode());
                if (updateRTRankTable.GetStatusCode() == "200")
                {
                    Get_power_ranking();
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                }
                else if (updateRTRankTable.GetStatusCode() == "428")
                {
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(true);
                    Debug.Log("집계중입니다");
                    server_loading = false;
                }
                else
                {
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                    server_loading = false;
                    error_type = 11;
                    Server_connect_check();
                    send_log_server_error(error_type, updateRTRankTable.GetStatusCode().ToString(), updateRTRankTable.GetMessage().ToString());
                }
            }
        }

    }

    public void Update_best_stage_raking()
    {
        Debug.Log("Update_best_stage_raking in");

        server_loading_time = 0;
        server_loading = true;

        error_type = 13;
        if (gm)
        {
            Backend.GameInfo.UpdateRTRankTable("user_info", "best_stage", DBManager.Instance.user_info.inDate, 0, (callback) =>
            {
                Debug.Log("best_stage =================" + callback.GetStatusCode());
                if (callback.GetStatusCode() == "200")
                {
                    Get_best_stage_ranking();
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                }
                else if (callback.GetStatusCode() == "428")
                {
                    Debug.Log("집계중입니다");
                    server_loading = false;
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(true);
                }
                else
                {
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                    server_loading = false;
                    error_type = 13;
                    Server_connect_check();
                    send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());

                }
                // 이후 처리
            });
        }
        else
        {
            Backend.GameInfo.UpdateRTRankTable("user_info", "best_stage", DBManager.Instance.user_info.inDate, DBManager.Instance.obscurelong_converter(DBManager.Instance.user_info.best_stage), (callback) =>
            {
                Debug.Log("best_stage =================" + callback.GetStatusCode());
                if (callback.GetStatusCode() == "200")
                {
                    Get_best_stage_ranking();
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                }
                else if (callback.GetStatusCode() == "428")
                {
                    Debug.Log("집계중입니다");
                    server_loading = false;
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(true);
                }
                else
                {
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                    server_loading = false;
                    error_type = 13;
                    Server_connect_check();
                    send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());

                }
                // 이후 처리
            });
        }
    }
    public void Update_total_stage_ranking()
    {
        Debug.Log("Update_total_stage_ranking in");

        server_loading_time = 0;
        server_loading = true;

        error_type = 12;
        if (gm)
        {
            Backend.GameInfo.UpdateRTRankTable("user_info", "total_stage", DBManager.Instance.user_info.inDate, 0, (callback) =>
            {
                Debug.Log("total_stage =================" + callback.GetStatusCode());
                if (callback.GetStatusCode() == "200")
                {
                    Get_total_stage_ranking();
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                }
                else if (callback.GetStatusCode() == "428")
                {
                    Debug.Log("집계중입니다");
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(true);
                    server_loading = false;
                }
                else
                {
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                    server_loading = false;
                    error_type = 12;
                    Server_connect_check();
                    send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
                }
                // 이후 처리
            });
        }
        else
        {
            Backend.GameInfo.UpdateRTRankTable("user_info", "total_stage", DBManager.Instance.user_info.inDate, DBManager.Instance.obscureint_converter(DBManager.Instance.user_info.total_stage), (callback) =>
            {
                Debug.Log("total_stage =================" + callback.GetStatusCode());
                if (callback.GetStatusCode() == "200")
                {
                    Get_total_stage_ranking();
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                }
                else if (callback.GetStatusCode() == "428")
                {
                    Debug.Log("집계중입니다");
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(true);
                    server_loading = false;
                }
                else
                {
                    UIManager.Instance.ranking_panel.ready_text.gameObject.SetActive(false);
                    server_loading = false;
                    error_type = 12;
                    Server_connect_check();
                    send_log_server_error(error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
                }
                // 이후 처리
            });
        }
    }

    public void Insert_achievement() // 공용 업적 인서트
    {
        Debug.LogError("insertAchievement");
        Param param4 = new Param();
        param4.Add("index", "0");
        param4.Add("name", "몬스터 처치");
        param4.Add("info", "몬스터를 처치 합니다.");
        param4.Add("target", "1,2,3,4,5,6,7,8,9,10,11,12,13,15,16,17,18,19,20");
        param4.Add("reward", "5,10,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,95,100");
        BackendReturnObject insert4 = Backend.GameData.Insert(achievement_table, param4);
        Debug.Log("insert" + insert4);
        if (insert4.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert4);
        }
        Param param5 = new Param();
        param5.Add("index", "1");
        param5.Add("name", "보스 처치");
        param5.Add("info", "보스를 처치 합니다..");
        param5.Add("target", "1,2,3,4,5,6,7,8,9,10,11,12,13,15,16,17,18,19,20");
        param5.Add("reward", "5,10,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,95,100");
        BackendReturnObject insert5 = Backend.GameData.Insert(achievement_table, param5);
        Debug.Log("insert" + insert5);
        if (insert5.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert5);
        }
        Param param6 = new Param();
        param6.Add("index", "2");
        param6.Add("name", "광고 중독");
        param6.Add("info", "광고 시청을 통해 보상을 획득합니다.");
        param6.Add("target", "1,2,3,4,5,6,7,8,9,10,11,12,13,15,16,17,18,19,20");
        param6.Add("reward", "5,10,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,95,100");
        BackendReturnObject insert6 = Backend.GameData.Insert(achievement_table, param6);
        Debug.Log("insert" + insert6);
        if (insert6.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert6);
        }
        Param param7 = new Param();
        param7.Add("index", "3");
        param7.Add("name", "개장수");
        param7.Add("info", "다양한 종류의 개를 획득합니다.");
        param7.Add("target", "1,2,3,4,5,6,7,8,9,10,11,12,13,15,16,17,18,19,20");
        param7.Add("reward", "5,10,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,95,100");
        BackendReturnObject insert7 = Backend.GameData.Insert(achievement_table, param7);
        Debug.Log("insert" + insert7);
        if (insert7.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert7);
        }
        Param param8 = new Param();
        param8.Add("index", "4");
        param8.Add("name", "등산왕");
        param8.Add("info", "최고 스테이지에 도달 합니다.");
        param8.Add("target", "1,2,3,4,5,6,7,8,9,10,11,12,13,15,16,17,18,19,20");
        param8.Add("reward", "5,10,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,95,100");
        BackendReturnObject insert8 = Backend.GameData.Insert(achievement_table, param8);
        Debug.Log("insert" + insert8);
        if (insert8.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert8);
        }
        Param param9 = new Param();
        param9.Add("index", "5");
        param9.Add("name", "환생하기");
        param9.Add("info", "환생을 합니다.");
        param9.Add("target", "1,2,3,4,5,6,7,8,9,10,11,12,13,15,16,17,18,19,20");
        param9.Add("reward", "5,10,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,95,100");
        BackendReturnObject insert9 = Backend.GameData.Insert(achievement_table, param9);
        Debug.Log("insert" + insert9);
        if (insert9.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert9);
        }
        Param param10 = new Param();
        param10.Add("index", "6");
        param10.Add("name", "뽑기의신");
        param10.Add("info", "뽑기를 진행 합니다.");
        param10.Add("target", "1,2,3,4,5,6,7,8,9,10,11,12,13,15,16,17,18,19,20");
        param10.Add("reward", "5,10,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,95,100");
        BackendReturnObject insert10 = Backend.GameData.Insert(achievement_table, param10);
        Debug.Log("insert" + insert10);
        if (insert10.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert10);
        }
    }
    public void Insert_pet_collection() // 공용 콜렉션 인서트
    {
        Debug.LogError("insertPetcollection");
        Param param = new Param();
        param.Add("index", "0");
        param.Add("ability_index", "0");
        param.Add("ability_value", "10");
        param.Add("pet_grade", "0");
        param.Add("pet_1", "1");
        param.Add("pet_2", "0");
        param.Add("pet_3", "1");
        param.Add("pet_4", "0");

        BackendReturnObject insert = Backend.GameData.Insert(pet_collection_table, param);
        Debug.Log("insert" + insert);
        if (insert.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert);
        }
        Param param1 = new Param();
        param1.Add("index", "1");
        param1.Add("ability_index", "4");
        param1.Add("ability_value", "5");
        param1.Add("pet_grade", "0");
        param1.Add("pet_1", "0");
        param1.Add("pet_2", "1");
        param1.Add("pet_3", "0");
        param1.Add("pet_4", "1");

        BackendReturnObject insert1 = Backend.GameData.Insert(pet_collection_table, param1);
        Debug.Log("insert" + insert1);
        if (insert1.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert1);
        }
        Param param2 = new Param();
        param2.Add("index", "2");
        param2.Add("ability_index", "5");
        param2.Add("ability_value", "5");
        param2.Add("pet_grade", "0");
        param2.Add("pet_1", "1");
        param2.Add("pet_2", "1");
        param2.Add("pet_3", "1");
        param2.Add("pet_4", "1");

        BackendReturnObject insert2 = Backend.GameData.Insert(pet_collection_table, param2);
        Debug.Log("insert" + insert2);
        if (insert2.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert2);
        }

        Param param3 = new Param();
        param3.Add("index", "3");
        param3.Add("ability_index", "0");
        param3.Add("ability_value", "20");
        param3.Add("pet_grade", "1");
        param3.Add("pet_1", "1");
        param3.Add("pet_2", "0");
        param3.Add("pet_3", "1");
        param3.Add("pet_4", "0");

        BackendReturnObject insert3 = Backend.GameData.Insert(pet_collection_table, param3);
        Debug.Log("insert" + insert3);
        if (insert3.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert3);
        }

        Param param4 = new Param();
        param4.Add("index", "4");
        param4.Add("ability_index", "4");
        param4.Add("ability_value", "10");
        param4.Add("pet_grade", "1");
        param4.Add("pet_1", "0");
        param4.Add("pet_2", "1");
        param4.Add("pet_3", "0");
        param4.Add("pet_4", "1");

        BackendReturnObject insert4 = Backend.GameData.Insert(pet_collection_table, param4);
        Debug.Log("insert" + insert4);
        if (insert4.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert4);
        }

        Param param5 = new Param();
        param5.Add("index", "5");
        param5.Add("ability_index", "5");
        param5.Add("ability_value", "10");
        param5.Add("pet_grade", "1");
        param5.Add("pet_1", "1");
        param5.Add("pet_2", "1");
        param5.Add("pet_3", "1");
        param5.Add("pet_4", "1");

        BackendReturnObject insert5 = Backend.GameData.Insert(pet_collection_table, param5);
        Debug.Log("insert" + insert5);
        if (insert5.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert5);
        }

        Param param6 = new Param();
        param6.Add("index", "6");
        param6.Add("ability_index", "0");
        param6.Add("ability_value", "30");
        param6.Add("pet_grade", "2");
        param6.Add("pet_1", "1");
        param6.Add("pet_2", "0");
        param6.Add("pet_3", "1");
        param6.Add("pet_4", "0");

        BackendReturnObject insert6 = Backend.GameData.Insert(pet_collection_table, param6);
        Debug.Log("insert" + insert6);
        if (insert6.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert6);
        }

        Param param7 = new Param();
        param7.Add("index", "7");
        param7.Add("ability_index", "4");
        param7.Add("ability_value", "15");
        param7.Add("pet_grade", "2");
        param7.Add("pet_1", "0");
        param7.Add("pet_2", "1");
        param7.Add("pet_3", "0");
        param7.Add("pet_4", "1");

        BackendReturnObject insert7 = Backend.GameData.Insert(pet_collection_table, param7);
        Debug.Log("insert" + insert7);
        if (insert7.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert7);
        }

        Param param8 = new Param();
        param8.Add("index", "8");
        param8.Add("ability_index", "5");
        param8.Add("ability_value", "15");
        param8.Add("pet_grade", "2");
        param8.Add("pet_1", "1");
        param8.Add("pet_2", "1");
        param8.Add("pet_3", "1");
        param8.Add("pet_4", "1");

        BackendReturnObject insert8 = Backend.GameData.Insert(pet_collection_table, param8);
        Debug.Log("insert" + insert8);
        if (insert8.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert8);
        }

        Param param9 = new Param();
        param9.Add("index", "9");
        param9.Add("ability_index", "0");
        param9.Add("ability_value", "40");
        param9.Add("pet_grade", "3");
        param9.Add("pet_1", "1");
        param9.Add("pet_2", "0");
        param9.Add("pet_3", "1");
        param9.Add("pet_4", "0");

        BackendReturnObject insert9 = Backend.GameData.Insert(pet_collection_table, param9);
        Debug.Log("insert" + insert9);
        if (insert9.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert9);
        }

        Param param10 = new Param();
        param10.Add("index", "10");
        param10.Add("ability_index", "4");
        param10.Add("ability_value", "20");
        param10.Add("pet_grade", "3");
        param10.Add("pet_1", "0");
        param10.Add("pet_2", "1");
        param10.Add("pet_3", "0");
        param10.Add("pet_4", "1");

        BackendReturnObject insert10 = Backend.GameData.Insert(pet_collection_table, param10);
        Debug.Log("insert" + insert10);
        if (insert10.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert10);
        }

        Param param11 = new Param();
        param11.Add("index", "11");
        param11.Add("ability_index", "5");
        param11.Add("ability_value", "20");
        param11.Add("pet_grade", "3");
        param11.Add("pet_1", "1");
        param11.Add("pet_2", "1");
        param11.Add("pet_3", "1");
        param11.Add("pet_4", "1");

        BackendReturnObject insert11 = Backend.GameData.Insert(pet_collection_table, param11);
        Debug.Log("insert" + insert11);
        if (insert11.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert11);
        }

        Param param12 = new Param();
        param12.Add("index", "12");
        param12.Add("ability_index", "0");
        param12.Add("ability_value", "50");
        param12.Add("pet_grade", "4");
        param12.Add("pet_1", "1");
        param12.Add("pet_2", "0");
        param12.Add("pet_3", "1");
        param12.Add("pet_4", "0");

        BackendReturnObject insert12 = Backend.GameData.Insert(pet_collection_table, param12);
        Debug.Log("insert" + insert12);
        if (insert12.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert12);
        }

        Param param13 = new Param();
        param13.Add("index", "13");
        param13.Add("ability_index", "4");
        param13.Add("ability_value", "25");
        param13.Add("pet_grade", "4");
        param13.Add("pet_1", "0");
        param13.Add("pet_2", "1");
        param13.Add("pet_3", "0");
        param13.Add("pet_4", "1");

        BackendReturnObject insert13 = Backend.GameData.Insert(pet_collection_table, param13);
        Debug.Log("insert" + insert13);
        if (insert13.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert13);
        }

        Param param14 = new Param();
        param14.Add("index", "14");
        param14.Add("ability_index", "5");
        param14.Add("ability_value", "25");
        param14.Add("pet_grade", "4");
        param14.Add("pet_1", "1");
        param14.Add("pet_2", "1");
        param14.Add("pet_3", "1");
        param14.Add("pet_4", "1");

        BackendReturnObject insert14 = Backend.GameData.Insert(pet_collection_table, param14);
        Debug.Log("insert" + insert14);
        if (insert14.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert14);
        }

        Param param15 = new Param();
        param15.Add("index", "15");
        param15.Add("ability_index", "0");
        param15.Add("ability_value", "60");
        param15.Add("pet_grade", "5");
        param15.Add("pet_1", "1");
        param15.Add("pet_2", "0");
        param15.Add("pet_3", "1");
        param15.Add("pet_4", "0");

        BackendReturnObject insert15 = Backend.GameData.Insert(pet_collection_table, param15);
        Debug.Log("insert" + insert15);
        if (insert15.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert15);
        }

        Param param16 = new Param();
        param16.Add("index", "16");
        param16.Add("ability_index", "4");
        param16.Add("ability_value", "30");
        param16.Add("pet_grade", "5");
        param16.Add("pet_1", "0");
        param16.Add("pet_2", "1");
        param16.Add("pet_3", "0");
        param16.Add("pet_4", "1");

        BackendReturnObject insert16 = Backend.GameData.Insert(pet_collection_table, param16);
        Debug.Log("insert" + insert16);
        if (insert16.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert16);
        }

        Param param17 = new Param();
        param17.Add("index", "17");
        param17.Add("ability_index", "5");
        param17.Add("ability_value", "30");
        param17.Add("pet_grade", "5");
        param17.Add("pet_1", "1");
        param17.Add("pet_2", "1");
        param17.Add("pet_3", "1");
        param17.Add("pet_4", "1");

        BackendReturnObject insert17 = Backend.GameData.Insert(pet_collection_table, param17);
        Debug.Log("insert" + insert17);
        if (insert17.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert17);
        }

        Param param18 = new Param();
        param18.Add("index", "18");
        param18.Add("ability_index", "0");
        param18.Add("ability_value", "100");
        param18.Add("pet_grade", "6");
        param18.Add("pet_1", "1");
        param18.Add("pet_2", "0");
        param18.Add("pet_3", "1");
        param18.Add("pet_4", "0");

        BackendReturnObject insert18 = Backend.GameData.Insert(pet_collection_table, param18);
        Debug.Log("insert" + insert18);
        if (insert18.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert18);
        }

        Param param19 = new Param();
        param19.Add("index", "19");
        param19.Add("ability_index", "4");
        param19.Add("ability_value", "50");
        param19.Add("pet_grade", "6");
        param19.Add("pet_1", "0");
        param19.Add("pet_2", "1");
        param19.Add("pet_3", "0");
        param19.Add("pet_4", "1");

        BackendReturnObject insert19 = Backend.GameData.Insert(pet_collection_table, param19);
        Debug.Log("insert" + insert19);
        if (insert19.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert19);
        }

        Param param20 = new Param();
        param20.Add("index", "20");
        param20.Add("ability_index", "5");
        param20.Add("ability_value", "35");
        param20.Add("pet_grade", "6");
        param20.Add("pet_1", "1");
        param20.Add("pet_2", "1");
        param20.Add("pet_3", "1");
        param20.Add("pet_4", "1");

        BackendReturnObject insert20 = Backend.GameData.Insert(pet_collection_table, param20);
        Debug.Log("insert" + insert20);
        if (insert20.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert20);
        }

        Param param21 = new Param();
        param21.Add("index", "21");
        param21.Add("ability_index", "0");
        param21.Add("ability_value", "200");
        param21.Add("pet_grade", "7");
        param21.Add("pet_1", "1");
        param21.Add("pet_2", "0");
        param21.Add("pet_3", "1");
        param21.Add("pet_4", "0");

        BackendReturnObject insert21 = Backend.GameData.Insert(pet_collection_table, param21);
        Debug.Log("insert" + insert21);
        if (insert21.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert21);
        }

        Param param22 = new Param();
        param22.Add("index", "22");
        param22.Add("ability_index", "4");
        param22.Add("ability_value", "100");
        param22.Add("pet_grade", "7");
        param22.Add("pet_1", "0");
        param22.Add("pet_2", "1");
        param22.Add("pet_3", "0");
        param22.Add("pet_4", "1");

        BackendReturnObject insert22 = Backend.GameData.Insert(pet_collection_table, param22);
        Debug.Log("insert" + insert22);
        if (insert22.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert22);
        }

        Param param23 = new Param();
        param23.Add("index", "23");
        param23.Add("ability_index", "5");
        param23.Add("ability_value", "40");
        param23.Add("pet_grade", "7");
        param23.Add("pet_1", "1");
        param23.Add("pet_2", "1");
        param23.Add("pet_3", "1");
        param23.Add("pet_4", "1");

        BackendReturnObject insert23 = Backend.GameData.Insert(pet_collection_table, param23);
        Debug.Log("insert" + insert23);
        if (insert23.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert23);
        }
    }
    public void Insert_collection() // 공용 콜렉션 인서트
    {
        Debug.LogError("insertCollection");
        Param param = new Param();
        param.Add("index", "0");
        param.Add("name", "0");
        param.Add("ability_index", "0");
        param.Add("ability_value", "1");
        param.Add("unit_1", "0");
        param.Add("unit_2", "1");
        param.Add("unit_3", "2");
        param.Add("unit_4", "3");
        param.Add("unit_5", "4");
        BackendReturnObject insert = Backend.GameData.Insert(collection_table, param);
        Debug.Log("insert" + insert);
        if (insert.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert);
        }
        Param param1 = new Param();
        param1.Add("index", "1");
        param1.Add("name", "1");
        param1.Add("ability_index", "2");
        param1.Add("ability_value", "1");
        param1.Add("unit_1", "0");
        param1.Add("unit_2", "2");
        param1.Add("unit_3", "4");
        param1.Add("unit_4", "null");
        param1.Add("unit_5", "null");
        BackendReturnObject insert1 = Backend.GameData.Insert(collection_table, param1);
        Debug.Log("insert" + insert1);
        if (insert1.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert1);
        }
        Param param2 = new Param();
        param2.Add("index", "2");
        param2.Add("name", "2");
        param2.Add("ability_index", "4");
        param2.Add("ability_value", "1");
        param2.Add("unit_1", "1");
        param2.Add("unit_2", "3");
        param2.Add("unit_3", "null");
        param2.Add("unit_4", "null");
        param2.Add("unit_5", "null");
        BackendReturnObject insert2 = Backend.GameData.Insert(collection_table, param2);
        Debug.Log("insert" + insert);
        if (insert2.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert2);
        }
        Param param3 = new Param();
        param3.Add("index", "3");
        param3.Add("name", "3");
        param3.Add("ability_index", "0");
        param3.Add("ability_value", "2");
        param3.Add("unit_1", "5");
        param3.Add("unit_2", "6");
        param3.Add("unit_3", "7");
        param3.Add("unit_4", "8");
        param3.Add("unit_5", "9");
        BackendReturnObject insert3 = Backend.GameData.Insert(collection_table, param3);
        Debug.Log("insert" + insert3);
        if (insert3.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert3);
        }
        Param param4 = new Param();
        param4.Add("index", "4");
        param4.Add("name", "4");
        param4.Add("ability_index", "2");
        param4.Add("ability_value", "2");
        param4.Add("unit_1", "5");
        param4.Add("unit_2", "7");
        param4.Add("unit_3", "9");
        param4.Add("unit_4", "null");
        param4.Add("unit_5", "null");
        BackendReturnObject insert4 = Backend.GameData.Insert(collection_table, param4);
        Debug.Log("insert" + insert4);
        if (insert4.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert4);
        }
        Param param5 = new Param();
        param5.Add("index", "5");
        param5.Add("name", "5");
        param5.Add("ability_index", "4");
        param5.Add("ability_value", "2");
        param5.Add("unit_1", "6");
        param5.Add("unit_2", "8");
        param5.Add("unit_3", "null");
        param5.Add("unit_4", "null");
        param5.Add("unit_5", "null");
        BackendReturnObject insert5 = Backend.GameData.Insert(collection_table, param5);
        Debug.Log("insert" + insert5);
        if (insert5.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert5);
        }
        Param param6 = new Param();
        param6.Add("index", "6");
        param6.Add("name", "6");
        param6.Add("ability_index", "0");
        param6.Add("ability_value", "3");
        param6.Add("unit_1", "10");
        param6.Add("unit_2", "11");
        param6.Add("unit_3", "12");
        param6.Add("unit_4", "13");
        param6.Add("unit_5", "14");
        BackendReturnObject insert6 = Backend.GameData.Insert(collection_table, param6);
        Debug.Log("insert" + insert6);
        if (insert6.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert6);
        }
        Param param7 = new Param();
        param7.Add("index", "7");
        param7.Add("name", "7");
        param7.Add("ability_index", "2");
        param7.Add("ability_value", "3");
        param7.Add("unit_1", "10");
        param7.Add("unit_2", "12");
        param7.Add("unit_3", "14");
        param7.Add("unit_4", "null");
        param7.Add("unit_5", "null");
        BackendReturnObject insert7 = Backend.GameData.Insert(collection_table, param7);
        Debug.Log("insert" + insert7);
        if (insert7.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert7);
        }
        Param param8 = new Param();
        param8.Add("index", "8");
        param8.Add("name", "8");
        param8.Add("ability_index", "4");
        param8.Add("ability_value", "3");
        param8.Add("unit_1", "11");
        param8.Add("unit_2", "13");
        param8.Add("unit_3", "null");
        param8.Add("unit_4", "null");
        param8.Add("unit_5", "null");
        BackendReturnObject insert8 = Backend.GameData.Insert(collection_table, param8);
        Debug.Log("insert" + insert8);
        if (insert8.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert8);
        }
        Param param9 = new Param();
        param9.Add("index", "9");
        param9.Add("name", "9");
        param9.Add("ability_index", "0");
        param9.Add("ability_value", "4");
        param9.Add("unit_1", "15");
        param9.Add("unit_2", "16");
        param9.Add("unit_3", "17");
        param9.Add("unit_4", "18");
        param9.Add("unit_5", "19");
        BackendReturnObject insert9 = Backend.GameData.Insert(collection_table, param9);
        Debug.Log("insert" + insert9);
        if (insert9.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert9);
        }
        Param param10 = new Param();
        param10.Add("index", "10");
        param10.Add("name", "10");
        param10.Add("ability_index", "2");
        param10.Add("ability_value", "4");
        param10.Add("unit_1", "15");
        param10.Add("unit_2", "17");
        param10.Add("unit_3", "19");
        param10.Add("unit_4", "null");
        param10.Add("unit_5", "null");
        BackendReturnObject insert10 = Backend.GameData.Insert(collection_table, param10);
        Debug.Log("insert" + insert10);
        if (insert10.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert10);
        }
        Param param11 = new Param();
        param11.Add("index", "11");
        param11.Add("name", "11");
        param11.Add("ability_index", "4");
        param11.Add("ability_value", "4");
        param11.Add("unit_1", "16");
        param11.Add("unit_2", "18");
        param11.Add("unit_3", "null");
        param11.Add("unit_4", "null");
        param11.Add("unit_5", "null");
        BackendReturnObject insert11 = Backend.GameData.Insert(collection_table, param11);
        Debug.Log("insert" + insert11);
        if (insert11.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert11);
        }
        Param param12 = new Param();
        param12.Add("index", "12");
        param12.Add("name", "12");
        param12.Add("ability_index", "0");
        param12.Add("ability_value", "5");
        param12.Add("unit_1", "20");
        param12.Add("unit_2", "21");
        param12.Add("unit_3", "22");
        param12.Add("unit_4", "23");
        param12.Add("unit_5", "24");
        BackendReturnObject insert12 = Backend.GameData.Insert(collection_table, param12);
        Debug.Log("insert" + insert12);
        if (insert12.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert12);
        }
        Param param13 = new Param();
        param13.Add("index", "13");
        param13.Add("name", "13");
        param13.Add("ability_index", "2");
        param13.Add("ability_value", "5");
        param13.Add("unit_1", "20");
        param13.Add("unit_2", "22");
        param13.Add("unit_3", "24");
        param13.Add("unit_4", "null");
        param13.Add("unit_5", "null");
        BackendReturnObject insert13 = Backend.GameData.Insert(collection_table, param13);
        Debug.Log("insert" + insert13);
        if (insert13.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert13);
        }
        Param param14 = new Param();
        param14.Add("index", "14");
        param14.Add("name", "14");
        param14.Add("ability_index", "4");
        param14.Add("ability_value", "5");
        param14.Add("unit_1", "21");
        param14.Add("unit_2", "23");
        param14.Add("unit_3", "null");
        param14.Add("unit_4", "null");
        param14.Add("unit_5", "null");
        BackendReturnObject insert14 = Backend.GameData.Insert(collection_table, param14);
        Debug.Log("insert" + insert14);
        if (insert14.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert14);
        }
        Param param15 = new Param();
        param15.Add("index", "15");
        param15.Add("name", "15");
        param15.Add("ability_index", "0");
        param15.Add("ability_value", "6");
        param15.Add("unit_1", "25");
        param15.Add("unit_2", "26");
        param15.Add("unit_3", "27");
        param15.Add("unit_4", "28");
        param15.Add("unit_5", "29");
        BackendReturnObject insert15 = Backend.GameData.Insert(collection_table, param15);
        Debug.Log("insert" + insert15);
        if (insert15.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert15);
        }
        Param param16 = new Param();
        param16.Add("index", "16");
        param16.Add("name", "16");
        param16.Add("ability_index", "2");
        param16.Add("ability_value", "6");
        param16.Add("unit_1", "25");
        param16.Add("unit_2", "27");
        param16.Add("unit_3", "29");
        param16.Add("unit_4", "null");
        param16.Add("unit_5", "null");
        BackendReturnObject insert16 = Backend.GameData.Insert(collection_table, param16);
        Debug.Log("insert" + insert16);
        if (insert16.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert16);
        }
        Param param17 = new Param();
        param17.Add("index", "17");
        param17.Add("name", "17");
        param17.Add("ability_index", "4");
        param17.Add("ability_value", "6");
        param17.Add("unit_1", "26");
        param17.Add("unit_2", "28");
        param17.Add("unit_3", "null");
        param17.Add("unit_4", "null");
        param17.Add("unit_5", "null");
        BackendReturnObject insert17 = Backend.GameData.Insert(collection_table, param17);
        Debug.Log("insert" + insert17);
        if (insert17.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert17);
        }
        Param param18 = new Param();
        param18.Add("index", "18");
        param18.Add("name", "18");
        param18.Add("ability_index", "1");
        param18.Add("ability_value", "1");
        param18.Add("unit_1", "0");
        param18.Add("unit_2", "5");
        param18.Add("unit_3", "10");
        param18.Add("unit_4", "null");
        param18.Add("unit_5", "null");
        BackendReturnObject insert18 = Backend.GameData.Insert(collection_table, param18);
        Debug.Log("insert" + insert18);
        if (insert18.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert18);
        }
        Param param19 = new Param();
        param19.Add("index", "19");
        param19.Add("name", "19");
        param19.Add("ability_index", "1");
        param19.Add("ability_value", "1");
        param19.Add("unit_1", "1");
        param19.Add("unit_2", "11");
        param19.Add("unit_3", "16");
        param19.Add("unit_4", "null");
        param19.Add("unit_5", "null");
        BackendReturnObject insert19 = Backend.GameData.Insert(collection_table, param19);
        Debug.Log("insert" + insert18);
        if (insert19.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert19);
        }
        Param param20 = new Param();
        param20.Add("index", "20");
        param20.Add("name", "20");
        param20.Add("ability_index", "1");
        param20.Add("ability_value", "1");
        param20.Add("unit_1", "2");
        param20.Add("unit_2", "12");
        param20.Add("unit_3", "17");
        param20.Add("unit_4", "null");
        param20.Add("unit_5", "null");
        BackendReturnObject insert20 = Backend.GameData.Insert(collection_table, param20);
        Debug.Log("insert" + insert20);
        if (insert20.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert20);
        }
        Param param21 = new Param();
        param21.Add("index", "21");
        param21.Add("name", "21");
        param21.Add("ability_index", "1");
        param21.Add("ability_value", "1");
        param21.Add("unit_1", "3");
        param21.Add("unit_2", "13");
        param21.Add("unit_3", "18");
        param21.Add("unit_4", "null");
        param21.Add("unit_5", "null");
        BackendReturnObject insert21 = Backend.GameData.Insert(collection_table, param21);
        Debug.Log("insert" + insert21);
        if (insert21.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert21);
        }
        Param param22 = new Param();
        param22.Add("index", "22");
        param22.Add("name", "22");
        param22.Add("ability_index", "1");
        param22.Add("ability_value", "1");
        param22.Add("unit_1", "4");
        param22.Add("unit_2", "14");
        param22.Add("unit_3", "19");
        param22.Add("unit_4", "null");
        param22.Add("unit_5", "null");
        BackendReturnObject insert22 = Backend.GameData.Insert(collection_table, param22);
        Debug.Log("insert" + insert22);
        if (insert22.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert22);
        }
        Param param23 = new Param();
        param23.Add("index", "23");
        param23.Add("name", "23");
        param23.Add("ability_index", "5");
        param23.Add("ability_value", "1");
        param23.Add("unit_1", "20");
        param23.Add("unit_2", "25");
        param23.Add("unit_3", "null");
        param23.Add("unit_4", "null");
        param23.Add("unit_5", "null");
        BackendReturnObject insert23 = Backend.GameData.Insert(collection_table, param23);
        Debug.Log("insert" + insert23);
        if (insert23.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert23);
        }
        Param param24 = new Param();
        param24.Add("index", "24");
        param24.Add("name", "24");
        param24.Add("ability_index", "5");
        param24.Add("ability_value", "1");
        param24.Add("unit_1", "21");
        param24.Add("unit_2", "26");
        param24.Add("unit_3", "null");
        param24.Add("unit_4", "null");
        param24.Add("unit_5", "null");
        BackendReturnObject insert24 = Backend.GameData.Insert(collection_table, param24);
        Debug.Log("insert" + insert24);
        if (insert24.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert24);
        }
        Param param25 = new Param();
        param25.Add("index", "25");
        param25.Add("name", "25");
        param25.Add("ability_index", "5");
        param25.Add("ability_value", "1");
        param25.Add("unit_1", "22");
        param25.Add("unit_2", "27");
        param25.Add("unit_3", "null");
        param25.Add("unit_4", "null");
        param25.Add("unit_5", "null");
        BackendReturnObject insert25 = Backend.GameData.Insert(collection_table, param25);
        Debug.Log("insert" + insert25);
        if (insert25.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert25);
        }
        Param param26 = new Param();
        param26.Add("index", "26");
        param26.Add("name", "26");
        param26.Add("ability_index", "5");
        param26.Add("ability_value", "1");
        param26.Add("unit_1", "23");
        param26.Add("unit_2", "28");
        param26.Add("unit_3", "null");
        param26.Add("unit_4", "null");
        param26.Add("unit_5", "null");
        BackendReturnObject insert26 = Backend.GameData.Insert(collection_table, param26);
        Debug.Log("insert" + insert26);
        if (insert26.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert26);
        }
        Param param27 = new Param();
        param27.Add("index", "27");
        param27.Add("name", "27");
        param27.Add("ability_index", "5");
        param27.Add("ability_value", "1");
        param27.Add("unit_1", "24");
        param27.Add("unit_2", "29");
        param27.Add("unit_3", "null");
        param27.Add("unit_4", "null");
        param27.Add("unit_5", "null");
        BackendReturnObject insert27 = Backend.GameData.Insert(collection_table, param27);
        Debug.Log("insert" + insert27);
        if (insert27.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert27);
        }
    }
    public void Insert_unit() // 공용 개 인서트
    {
        Debug.LogError("insertDog");
        Param param = new Param();
        param.Add("index", "0");
        param.Add("grade", "0");
        param.Add("name", "똥개1");
        param.Add("info", "똥개1");
        param.Add("damage", "100");
        param.Add("speed", "120"); // 초당 공격 횟수
        param.Add("critical_percent", "1"); // 크리티컬 공격 확률 100 = 10%
        param.Add("critical_damage", "200"); // 크리티컬 데미지 일반 공격 2배
        param.Add("life", "100");  // 생명력
        param.Add("ability_index", "2");  // 특수 능력 번호
        param.Add("ability_value", "20"); // 특수 능력 수치
        BackendReturnObject insert = Backend.GameData.Insert(unit_table, param);
        Debug.Log("insert" + insert);
        if (insert.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert);
        }
        Param param1 = new Param();
        param1.Add("index", "1");
        param1.Add("grade", "0");
        param1.Add("name", "진돗개1");
        param1.Add("info", "진돗개1");
        param1.Add("damage", "120");
        param1.Add("speed", "120");
        param1.Add("critical_percent", "1");
        param1.Add("critical_damage", "240");
        param1.Add("life", "180");
        param1.Add("ability_index", "3");
        param1.Add("ability_value", "5");
        BackendReturnObject insert1 = Backend.GameData.Insert(unit_table, param1);
        Debug.Log("insert" + insert1);
        if (insert1.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert1);
        }
        Param param2 = new Param();
        param2.Add("index", "2");
        param2.Add("grade", "0");
        param2.Add("name", "코기1");
        param2.Add("info", "코기1");
        param2.Add("damage", "130");
        param2.Add("speed", "120");
        param2.Add("critical_percent", "1");
        param2.Add("critical_damage", "260");
        param2.Add("life", "150");
        param2.Add("ability_index", "1");
        param2.Add("ability_value", "5");
        BackendReturnObject insert2 = Backend.GameData.Insert(unit_table, param2);
        Debug.Log("insert" + insert2);
        if (insert2.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert2);
        }
        Param param3 = new Param();
        param3.Add("index", "3");
        param3.Add("grade", "0");
        param3.Add("name", "불독1");
        param3.Add("info", "불독1");
        param3.Add("damage", "200");
        param3.Add("speed", "120");
        param3.Add("critical_percent", "1");
        param3.Add("critical_damage", "400");
        param3.Add("life", "100");
        param3.Add("ability_index", "0");
        param3.Add("ability_value", "10");
        BackendReturnObject insert3 = Backend.GameData.Insert(unit_table, param3);
        Debug.Log("insert" + insert3);
        if (insert3.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert3);
        }
        Param param4 = new Param();
        param4.Add("index", "4");
        param4.Add("grade", "0");
        param4.Add("name", "리버1");
        param4.Add("info", "리버1");
        param4.Add("damage", "100");
        param4.Add("speed", "120");
        param4.Add("critical_percent", "1");
        param4.Add("critical_damage", "200");
        param4.Add("life", "200");
        param4.Add("ability_index", "4");
        param4.Add("ability_value", "5");
        BackendReturnObject insert4 = Backend.GameData.Insert(unit_table, param4);
        Debug.Log("insert" + insert4);
        if (insert4.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert4);
        }
        Param param5 = new Param();
        param5.Add("index", "5");
        param5.Add("grade", "1");
        param5.Add("name", "변개2");
        param5.Add("info", "변개2");
        param5.Add("damage", "200");
        param5.Add("speed", "120");
        param5.Add("critical_percent", "1");
        param5.Add("critical_damage", "400");
        param5.Add("life", "200");
        param5.Add("ability_index", "2");
        param5.Add("ability_value", "40");
        BackendReturnObject insert5 = Backend.GameData.Insert(unit_table, param5);
        Debug.Log("insert" + insert5);
        if (insert5.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert5);
        }
        Param param6 = new Param();
        param6.Add("index", "6");
        param6.Add("grade", "1");
        param6.Add("name", "진도2");
        param6.Add("info", "진도2");
        param6.Add("damage", "220");
        param6.Add("speed", "120");
        param6.Add("critical_percent", "1");
        param6.Add("critical_damage", "440");
        param6.Add("life", "330");
        param6.Add("ability_index", "3");
        param6.Add("ability_value", "10");
        BackendReturnObject insert6 = Backend.GameData.Insert(unit_table, param6);
        Debug.Log("insert" + insert6);
        if (insert6.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert6);
        }
        Param param7 = new Param();
        param7.Add("index", "7");
        param7.Add("grade", "1");
        param7.Add("name", "코기2");
        param7.Add("info", "코기2");
        param7.Add("damage", "230");
        param7.Add("speed", "120");
        param7.Add("critical_percent", "1");
        param7.Add("critical_damage", "460");
        param7.Add("life", "250");
        param7.Add("ability_index", "1");
        param7.Add("ability_value", "10");
        BackendReturnObject insert7 = Backend.GameData.Insert(unit_table, param7);
        Debug.Log("insert" + insert7);
        if (insert7.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert7);
        }
        Param param8 = new Param();
        param8.Add("index", "8");
        param8.Add("grade", "1");
        param8.Add("name", "불독2");
        param8.Add("info", "불독2");
        param8.Add("damage", "300");
        param8.Add("speed", "120");
        param8.Add("critical_percent", "1");
        param8.Add("critical_damage", "600");
        param8.Add("life", "200");
        param8.Add("ability_index", "0");
        param8.Add("ability_value", "20");
        BackendReturnObject insert8 = Backend.GameData.Insert(unit_table, param8);
        Debug.Log("insert" + insert8);
        if (insert8.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert8);
        }
        Param param9 = new Param();
        param9.Add("index", "9");
        param9.Add("grade", "1");
        param9.Add("name", "리버2");
        param9.Add("info", "리버2");
        param9.Add("damage", "200");
        param9.Add("speed", "120");
        param9.Add("critical_percent", "1");
        param9.Add("critical_damage", "400");
        param9.Add("life", "400");
        param9.Add("ability_index", "4");
        param9.Add("ability_value", "10");
        BackendReturnObject insert9 = Backend.GameData.Insert(unit_table, param9);
        Debug.Log("insert" + insert9);
        if (insert9.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert9);
        }
        Param param10 = new Param();
        param10.Add("index", "10");
        param10.Add("grade", "2");
        param10.Add("name", "변개3");
        param10.Add("info", "변개3");
        param10.Add("damage", "300");
        param10.Add("speed", "120");
        param10.Add("critical_percent", "1");
        param10.Add("critical_damage", "600");
        param10.Add("life", "300");
        param10.Add("ability_index", "2");
        param10.Add("ability_value", "80");
        BackendReturnObject insert10 = Backend.GameData.Insert(unit_table, param10);
        Debug.Log("insert" + insert);
        if (insert10.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert10);
        }
        Param param11 = new Param();
        param11.Add("index", "11");
        param11.Add("grade", "2");
        param11.Add("name", "진도3");
        param11.Add("info", "진도3");
        param11.Add("damage", "320");
        param11.Add("speed", "120");
        param11.Add("critical_percent", "1");
        param11.Add("critical_damage", "640");
        param11.Add("life", "480");
        param11.Add("ability_index", "3");
        param11.Add("ability_value", "15");
        BackendReturnObject insert11 = Backend.GameData.Insert(unit_table, param11);
        Debug.Log("insert" + insert11);
        if (insert11.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert11);
        }
        Param param12 = new Param();
        param12.Add("index", "12");
        param12.Add("grade", "2");
        param12.Add("name", "코기3");
        param12.Add("info", "코기3");
        param12.Add("damage", "340");
        param12.Add("speed", "120");
        param12.Add("critical_percent", "1");
        param12.Add("critical_damage", "680");
        param12.Add("life", "350");
        param12.Add("ability_index", "1");
        param12.Add("ability_value", "20");
        BackendReturnObject insert12 = Backend.GameData.Insert(unit_table, param12);
        Debug.Log("insert" + insert12);
        if (insert12.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert12);
        }
        Param param13 = new Param();
        param13.Add("index", "13");
        param13.Add("grade", "2");
        param13.Add("name", "불독3");
        param13.Add("info", "불독3");
        param13.Add("damage", "400");
        param13.Add("speed", "120");
        param13.Add("critical_percent", "1");
        param13.Add("critical_damage", "800");
        param13.Add("life", "300");
        param13.Add("ability_index", "0");
        param13.Add("ability_value", "40");
        BackendReturnObject insert13 = Backend.GameData.Insert(unit_table, param13);
        Debug.Log("insert" + insert13);
        if (insert13.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert13);
        }
        Param param14 = new Param();
        param14.Add("index", "14");
        param14.Add("grade", "2");
        param14.Add("name", "리버3");
        param14.Add("info", "리버3");
        param14.Add("damage", "300");
        param14.Add("speed", "120");
        param14.Add("critical_percent", "1");
        param14.Add("critical_damage", "600");
        param14.Add("life", "600");
        param14.Add("ability_index", "4");
        param14.Add("ability_value", "15");
        BackendReturnObject insert14 = Backend.GameData.Insert(unit_table, param14);
        Debug.Log("insert" + insert14);
        if (insert14.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert14);
        }
        Param param15 = new Param();
        param15.Add("index", "15");
        param15.Add("grade", "3");
        param15.Add("name", "변개4");
        param15.Add("info", "변개4");
        param15.Add("damage", "400");
        param15.Add("speed", "120");
        param15.Add("critical_percent", "1");
        param15.Add("critical_damage", "800");
        param15.Add("life", "400");
        param15.Add("ability_index", "2");
        param15.Add("ability_value", "120");
        BackendReturnObject insert15 = Backend.GameData.Insert(unit_table, param15);
        Debug.Log("insert" + insert15);
        if (insert15.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert15);
        }
        Param param16 = new Param();
        param16.Add("index", "16");
        param16.Add("grade", "3");
        param16.Add("name", "진도4");
        param16.Add("info", "진도4");
        param16.Add("damage", "420");
        param16.Add("speed", "120");
        param16.Add("critical_percent", "1");
        param16.Add("critical_damage", "840");
        param16.Add("life", "630");
        param16.Add("ability_index", "3");
        param16.Add("ability_value", "20");
        BackendReturnObject insert16 = Backend.GameData.Insert(unit_table, param16);
        Debug.Log("insert" + insert16);
        if (insert16.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert16);
        }
        Param param17 = new Param();
        param17.Add("index", "17");
        param17.Add("grade", "3");
        param17.Add("name", "코기4");
        param17.Add("info", "코기4");
        param17.Add("damage", "430");
        param17.Add("speed", "120");
        param17.Add("critical_percent", "1");
        param17.Add("critical_damage", "860");
        param17.Add("life", "450");
        param17.Add("ability_index", "1");
        param17.Add("ability_value", "30");
        BackendReturnObject insert17 = Backend.GameData.Insert(unit_table, param17);
        Debug.Log("insert" + insert17);
        if (insert17.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert17);
        }
        Param param18 = new Param();
        param18.Add("index", "18");
        param18.Add("grade", "3");
        param18.Add("name", "불독4");
        param18.Add("info", "불독4");
        param18.Add("damage", "550");
        param18.Add("speed", "120");
        param18.Add("critical_percent", "1");
        param18.Add("critical_damage", "1100");
        param18.Add("life", "400");
        param18.Add("ability_index", "0");
        param18.Add("ability_value", "60");
        BackendReturnObject insert18 = Backend.GameData.Insert(unit_table, param18);
        Debug.Log("insert" + insert18);
        if (insert18.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert18);
        }
        Param param19 = new Param();
        param19.Add("index", "19");
        param19.Add("grade", "3");
        param19.Add("name", "리버4");
        param19.Add("info", "리버4");
        param19.Add("damage", "400");
        param19.Add("speed", "120");
        param19.Add("critical_percent", "1");
        param19.Add("critical_damage", "800");
        param19.Add("life", "800");
        param19.Add("ability_index", "4");
        param19.Add("ability_value", "20");
        BackendReturnObject insert19 = Backend.GameData.Insert(unit_table, param19);
        Debug.Log("insert" + insert19);
        if (insert19.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert19);
        }
        Param param20 = new Param();
        param20.Add("index", "20");
        param20.Add("grade", "4");
        param20.Add("name", "변개5");
        param20.Add("info", "변개5");
        param20.Add("damage", "500");
        param20.Add("speed", "120");
        param20.Add("critical_percent", "1");
        param20.Add("critical_damage", "1000");
        param20.Add("life", "500");
        param20.Add("ability_index", "2");
        param20.Add("ability_value", "160");
        BackendReturnObject insert20 = Backend.GameData.Insert(unit_table, param20);
        Debug.Log("insert" + insert20);
        if (insert20.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert20);
        }
        Param param21 = new Param();
        param21.Add("index", "21");
        param21.Add("grade", "4");
        param21.Add("name", "진도5");
        param21.Add("info", "진도5");
        param21.Add("damage", "520");
        param21.Add("speed", "120");
        param21.Add("critical_percent", "1");
        param21.Add("critical_damage", "1040");
        param21.Add("life", "780");
        param21.Add("ability_index", "3");
        param21.Add("ability_value", "25");
        BackendReturnObject insert21 = Backend.GameData.Insert(unit_table, param21);
        Debug.Log("insert" + insert21);
        if (insert21.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert21);
        }
        Param param22 = new Param();
        param22.Add("index", "22");
        param22.Add("grade", "4");
        param22.Add("name", "코기5");
        param22.Add("info", "코기5");
        param22.Add("damage", "530");
        param22.Add("speed", "120");
        param22.Add("critical_percent", "1");
        param22.Add("critical_damage", "1060");
        param22.Add("life", "550");
        param22.Add("ability_index", "1");
        param22.Add("ability_value", "40");
        BackendReturnObject insert22 = Backend.GameData.Insert(unit_table, param22);
        Debug.Log("insert" + insert22);
        if (insert22.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert22);
        }
        Param param23 = new Param();
        param23.Add("index", "23");
        param23.Add("grade", "4");
        param23.Add("name", "불독5");
        param23.Add("info", "불독5");
        param23.Add("damage", "700");
        param23.Add("speed", "120");
        param23.Add("critical_percent", "1");
        param23.Add("critical_damage", "1400");
        param23.Add("life", "500");
        param23.Add("ability_index", "0");
        param23.Add("ability_value", "80");
        BackendReturnObject insert23 = Backend.GameData.Insert(unit_table, param23);
        Debug.Log("insert" + insert23);
        if (insert23.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert23);
        }
        Param param24 = new Param();
        param24.Add("index", "24");
        param24.Add("grade", "4");
        param24.Add("name", "리버5");
        param24.Add("info", "리버5");
        param24.Add("damage", "500");
        param24.Add("speed", "120");
        param24.Add("critical_percent", "1");
        param24.Add("critical_damage", "1000");
        param24.Add("life", "1000");
        param24.Add("ability_index", "4");
        param24.Add("ability_value", "25");
        BackendReturnObject insert24 = Backend.GameData.Insert(unit_table, param24);
        Debug.Log("insert" + insert24);
        if (insert24.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert24);
        }
        Param param25 = new Param();
        param25.Add("index", "25");
        param25.Add("grade", "5");
        param25.Add("name", "변개6");
        param25.Add("info", "변개6");
        param25.Add("damage", "600");
        param25.Add("speed", "120");
        param25.Add("critical_percent", "1");
        param25.Add("critical_damage", "1200");
        param25.Add("life", "600");
        param25.Add("ability_index", "2");
        param25.Add("ability_value", "200");
        BackendReturnObject insert25 = Backend.GameData.Insert(unit_table, param25);
        Debug.Log("insert" + insert25);
        if (insert25.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert25);
        }
        Param param26 = new Param();
        param26.Add("index", "26");
        param26.Add("grade", "5");
        param26.Add("name", "진도6");
        param26.Add("info", "진도6");
        param26.Add("damage", "620");
        param26.Add("speed", "120");
        param26.Add("critical_percent", "1");
        param26.Add("critical_damage", "1240");
        param26.Add("life", "930");
        param26.Add("ability_index", "3");
        param26.Add("ability_value", "30");
        BackendReturnObject insert26 = Backend.GameData.Insert(unit_table, param26);
        Debug.Log("insert" + insert26);
        if (insert26.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert26);
        }
        Param param27 = new Param();
        param27.Add("index", "27");
        param27.Add("grade", "5");
        param27.Add("name", "코기6");
        param27.Add("info", "코기6");
        param27.Add("damage", "630");
        param27.Add("speed", "120");
        param27.Add("critical_percent", "50");
        param27.Add("critical_damage", "1260");
        param27.Add("life", "650");
        param27.Add("ability_index", "1");
        param27.Add("ability_value", "50");
        BackendReturnObject insert27 = Backend.GameData.Insert(unit_table, param27);
        Debug.Log("insert" + insert27);
        if (insert27.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert27);
        }
        Param param28 = new Param();
        param28.Add("index", "28");
        param28.Add("grade", "5");
        param28.Add("name", "불독6");
        param28.Add("info", "불독6");
        param28.Add("damage", "900");
        param28.Add("speed", "120");
        param28.Add("critical_percent", "50");
        param28.Add("critical_damage", "1800");
        param28.Add("life", "600");
        param28.Add("ability_index", "0");
        param28.Add("ability_value", "100");
        BackendReturnObject insert28 = Backend.GameData.Insert(unit_table, param28);
        Debug.Log("insert" + insert28);
        if (insert28.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert28);
        }
        Param param29 = new Param();
        param29.Add("index", "29");
        param29.Add("grade", "5");
        param29.Add("name", "리버6");
        param29.Add("info", "리버6");
        param29.Add("damage", "600");
        param29.Add("speed", "120");
        param29.Add("critical_percent", "50");
        param29.Add("critical_damage", "1200");
        param29.Add("life", "1200");
        param29.Add("ability_index", "4");
        param29.Add("ability_value", "30");
        BackendReturnObject insert29 = Backend.GameData.Insert(unit_table, param29);
        Debug.Log("insert" + insert29);
        if (insert29.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert29);
        }


    }

    public void Insert_game_info() // 공용 겨험치 인서트
    {
        Param param = new Param();
        param.Add("exp", "20,30,50,80,120,170,230,300,380,470,580,780,1070,1450,1920,2480,3130,3870,4700,5620,6630,7730,8930,10230,11630,13130,14730,16430,18230,20130");
        param.Add("user_skill_level_value", "20,40,60,80,100/5,10,15,20,25/2,4,6,8,10/10,20,30,40,50/20,40,60,80,100/10,20,30,40,50");
        BackendReturnObject insert = Backend.GameData.Insert(game_info_table, param);
        Debug.Log("insert" + insert);
        if (insert.IsSuccess())
        {
            Debug.Log("insert _ exp success : " + insert);
        }
    }
    public void Insert_language()
    {
        Debug.Log("insert Insert_language");
        Param param = new Param();
        param.Add("korean", "0");
        param.Add("english", "0");
        param.Add("chinese_1", "0");
        param.Add("chinese_2", "0");
        param.Add("japanese", "0");
        BackendReturnObject insert = Backend.GameData.Insert(language_table, param);
        //Debug.Log("insert" + insert);
        if (insert.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert);
        }
    }
    public void Insert_upgrade() // 공용 강화 인서트
    {
        Param param = new Param();
        param.Add("index", "0");
        param.Add("name", "공격력 업그레이드");
        param.Add("info", "공격력을 강화 시킵니다.");
        param.Add("gold_price", "100");
        param.Add("dia_price", "10");
        param.Add("again_price", "10");
        BackendReturnObject insert = Backend.GameData.Insert(upgrade_table, param);
        Debug.Log("insert" + insert);
        if (insert.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert);
        }
        Param param1 = new Param();
        param1.Add("index", "1");
        param1.Add("name", "공격력 업그레이드");
        param1.Add("info", "공격력을 강화 시킵니다.");
        param1.Add("gold_price", "100");
        param1.Add("dia_price", "10");
        param1.Add("again_price", "10");
        BackendReturnObject insert1 = Backend.GameData.Insert(upgrade_table, param1);
        Debug.Log("insert" + insert1);
        if (insert1.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert1);
        }
        Param param2 = new Param();
        param2.Add("index", "2");
        param2.Add("name", "공격력 업그레이드");
        param2.Add("info", "공격력을 강화 시킵니다.");
        param2.Add("gold_price", "100");
        param2.Add("dia_price", "10");
        param2.Add("again_price", "10");
        BackendReturnObject insert2 = Backend.GameData.Insert(upgrade_table, param2);
        Debug.Log("insert" + insert2);
        if (insert2.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert2);
        }
        Param param3 = new Param();
        param3.Add("index", "3");
        param3.Add("name", "공격력 업그레이드");
        param3.Add("info", "공격력을 강화 시킵니다.");
        param3.Add("gold_price", "100");
        param3.Add("dia_price", "10");
        param3.Add("again_price", "10");
        BackendReturnObject insert3 = Backend.GameData.Insert(upgrade_table, param3);
        Debug.Log("insert" + insert3);
        if (insert3.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert3);
        }
        Param param4 = new Param();
        param4.Add("index", "4");
        param4.Add("name", "공격력 업그레이드");
        param4.Add("info", "공격력을 강화 시킵니다.");
        param4.Add("gold_price", "100");
        param4.Add("dia_price", "10");
        param4.Add("again_price", "10");
        BackendReturnObject insert4 = Backend.GameData.Insert(upgrade_table, param4);
        Debug.Log("insert" + insert4);
        if (insert4.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert4);
        }
        Param param5 = new Param();
        param5.Add("index", "5");
        param5.Add("name", "공격력 업그레이드");
        param5.Add("info", "공격력을 강화 시킵니다.");
        param5.Add("gold_price", "100");
        param5.Add("dia_price", "10");
        param5.Add("again_price", "10");
        BackendReturnObject insert5 = Backend.GameData.Insert(upgrade_table, param5);
        Debug.Log("insert" + insert5);
        if (insert5.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert5);
        }
        Param param6 = new Param();
        param6.Add("index", "6");
        param6.Add("name", "공격력 업그레이드");
        param6.Add("info", "공격력을 강화 시킵니다.");
        param6.Add("gold_price", "100");
        param6.Add("dia_price", "10");
        param6.Add("again_price", "10");
        BackendReturnObject insert6 = Backend.GameData.Insert(upgrade_table, param6);
        Debug.Log("insert" + insert6);
        if (insert6.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert6);
        }
        Param param7 = new Param();
        param7.Add("index", "7");
        param7.Add("name", "공격력 업그레이드");
        param7.Add("info", "공격력을 강화 시킵니다.");
        param7.Add("gold_price", "100");
        param7.Add("dia_price", "10");
        param7.Add("again_price", "10");
        BackendReturnObject insert7 = Backend.GameData.Insert(upgrade_table, param7);
        Debug.Log("insert" + insert7);
        if (insert7.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert7);
        }
    }

    public void Insert_quest()
    {
        Debug.Log("유저 데이터 없음 insert_userinfo");
        Param param = new Param();
        param.Add("quest_index", "0");
        param.Add("quest_name", "매일 로그인 하세요");
        param.Add("quest_reward", "10");
        param.Add("quest_target", "1");
        BackendReturnObject insert = Backend.GameData.Insert(quest_table, param);
        //Debug.Log("insert" + insert);
        if (insert.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert);
        }

        Param param1 = new Param();
        param1.Add("quest_index", "1");
        param1.Add("quest_name", "몬스터를 처치 하세요");
        param1.Add("quest_reward", "10");
        param1.Add("quest_target", "10");
        BackendReturnObject insert1 = Backend.GameData.Insert(quest_table, param1);
        //Debug.Log("insert" + insert);
        if (insert1.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert1);
        }

        Param param2 = new Param();
        param2.Add("quest_index", "2");
        param2.Add("quest_name", "보스를 처치 하세요");
        param2.Add("quest_reward", "10");
        param2.Add("quest_target", "1");
        BackendReturnObject insert2 = Backend.GameData.Insert(quest_table, param2);
        //Debug.Log("insert" + insert);
        if (insert2.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert2);
        }

        Param param3 = new Param();
        param3.Add("quest_index", "3");
        param3.Add("quest_name", "뽑기를 진행 하세요");
        param3.Add("quest_reward", "10");
        param3.Add("quest_target", "1");
        BackendReturnObject insert3 = Backend.GameData.Insert(quest_table, param3);
        //Debug.Log("insert" + insert);
        if (insert3.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert3);
        }

        Param param4 = new Param();
        param4.Add("quest_index", "4");
        param4.Add("quest_name", "환생을 진행 하세요");
        param4.Add("quest_reward", "10");
        param4.Add("quest_target", "1");
        BackendReturnObject insert4 = Backend.GameData.Insert(quest_table, param4);
        //Debug.Log("insert" + insert);
        if (insert4.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert4);
        }

        Param param5 = new Param();
        param5.Add("quest_index", "5");
        param5.Add("quest_name", "합성을 진행 하세요");
        param5.Add("quest_reward", "10");
        param5.Add("quest_target", "1");
        BackendReturnObject insert5 = Backend.GameData.Insert(quest_table, param5);
        //Debug.Log("insert" + insert);
        if (insert5.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert5);
        }
    }

    public void Insert_attendance() // 출석 체크 정보 인서트
    {
        Debug.Log("-------------Insert_attendance-----------");
        Param param = new Param();
        param.Add("attendance_reward_type", "0,3,4,0,2,1,0,3,4,0,2,1,0,3,4,0,2,1,0,3,4,0,2,1,0,3,4,0,2,1"); // 0: 골드 // 1: 다이아 // 2: 포인트 // 3: 아이템1 // 4: 아이템2
        param.Add("attendance_reward_value", "1000,1,1,2000,1000,100,3000,1,1,4000,1000,100,5000,1,1,6000,1000,100,7000,1,1,8000,1000,100,9000,1,1,10000,1000,100");
        param.Add("attendance_special_reward_type", "1,1,1");
        param.Add("attendance_special_reward_value", "100,100,100");
        BackendReturnObject insert = Backend.GameData.Insert(attendance_table, param);
        //Debug.Log("insert" + insert);
        if (insert.IsSuccess())
        {
            Debug.Log("Insert_shipinfo success : " + insert);
        }
    }
    public void Insert_userinfo(JsonData json)   // 유저 정보 인서트
    {
        Debug.Log("유저 데이터 없음 insert_userinfo");
        Param param = new Param();
        param.Add("gold", 0);
        param.Add("diamond", 0);
        param.Add("point", 0);
        param.Add("pvp_point", 1000);

        param.Add("user_unit", "1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
        param.Add("select_unit", 0);
        param.Add("user_stage", 1);

        param.Add("tresure_box_reward_time", "0");
        param.Add("review", "0");
        param.Add("notice_time", "0");
        param.Add("save_gold_time", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));

        param.Add("user_level", 1);
        param.Add("user_skill_point", 1);
        param.Add("user_experience", 0);
        param.Add("user_skill", "0,0,0,0,0,0");

        param.Add("user_item", "5,5,1,0,0");
        param.Add("user_item_time", "0,0,0,0");
        param.Add("user_item_ad_time", "0,0,0,0");

        param.Add("achievements", "0,0,0,0,0,0,0,0,0"); // monster_count,boss_count,ad_count,best_stage,total_stage,tresure_box_count,total_again
        param.Add("monster_count", 0);
        param.Add("boss_count", 0);
        param.Add("ad_count", 0);
        param.Add("best_stage", 0);
        param.Add("total_stage", 0);
        param.Add("tresure_box_count", 0);
        param.Add("total_again", 0);
        param.Add("power", 0);

        // 일일 임무
        param.Add("user_quest_value", "0,0,0,0,0,0,0,0"); //today_login,today_monster,today_boss,today_tresure_box,today_again,today_compose
        param.Add("user_quest_reward", "0,0,0,0,0,0,0,0"); //today_login,today_monster,today_boss,today_tresure_box,today_again,today_compose

        // 확정 합성 카운트
        param.Add("user_compose_count", "0,0,0,0,0,0,0"); // e,d,c,b,a,s,ss

        // 출석
        param.Add("attendance_check", 0);
        param.Add("attendance_time", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("attendance_count", 0);
        param.Add("attendance_reward", "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
        param.Add("attendance_level", 0);
        param.Add("attendance_special_reward", "0,0,0");

        // 업그레이드
        param.Add("upgrade_gold", "1,1,1,1,1,1,1");
        param.Add("upgrade_diamond", "1,1,1,1,1,1,1");
        param.Add("upgrade_point", "1,1,1,1,1,1,1");

        // PVP
        param.Add("pvp_time", "0");
        param.Add("pvp_ticket", "3");
        param.Add("pvp_ad_time", "0");

        param.Add("free_diamond_ticket", "10");
        param.Add("free_diamond_ad_time", "0");
        param.Add("point_treasure_ticket", "10");
        param.Add("package_active", "0");
        param.Add("package_active_time", "0");
        param.Add("start_package", "0");
        param.Add("gold_start_package", "0");

        // 자동환생 패키지
        param.Add("auto_again_active", "0");
        param.Add("auto_again_setting", "0");
        param.Add("auto_again_stage", "0");
        param.Add("auto_again_time", "0");

        //skin
        param.Add("user_skin_list", "0,0,0,0,0,0,0,0,0,0");
        param.Add("user_skin_state", "0");
        param.Add("user_current_skin", "0");

        //간식 무제한 패키지
        param.Add("item_package_purchase", "0"); //간식 패키지 구매 여부
        param.Add("item_package_time", "0"); //간식 패키지 사용기간

        //아이템 , 룬 교환 횟수
        param.Add("user_item_power_ad_count", "20");
        param.Add("user_item_speed_ad_count", "20");
        param.Add("user_rune_ad_count", "20");

        BackendReturnObject insert = Backend.GameData.Insert(user_info_table, param);

        if (insert.IsSuccess())
        {
            Debug.Log("------------------------Insert_userinfo success");
            User_nickname_check(json);
        }
        Insert_user_pvp();
    }

    public void Insert_user_pvp()
    {
        Debug.Log("Insert_user_pvp");
        Param param = new Param();
        param.Add("unit", 0);
        param.Add("nickname", "null");
        param.Add("attack_damage", 120);
        param.Add("attack_speed", 30);
        param.Add("critical_damage", 240);
        param.Add("critical_percent", 5);
        param.Add("life", 100);

        BackendReturnObject insert = Backend.GameData.Insert(user_pvp_table, param);
        //Debug.Log("insert" + insert);
        if (insert.IsSuccess())
        {
            Debug.Log("------------------------Insert_use_pvp success");
        }

    }

    public void Insert_new_user_boss()
    {
        Debug.Log("----- Insert_new_user_boss IN -----");
        Param param = new Param();
        param.Add("boss_index", "0,0,0,0,0");
        param.Add("boss_end_time", "0,0,0,0,0");
        param.Add("boss_life", "0,0,0,0,0");
        param.Add("boss_ticket", 3);
        param.Add("boss_ticket_time", "0");

        BackendReturnObject insert = Backend.GameData.Insert(user_new_boss_table, param);
        Debug.Log("Insert_user_boss" + insert.GetStatusCode());
        if (insert.IsSuccess())
        {
            Get_user_boss_info();
            Debug.Log("-- Insert_use_boss_ success --");
        }
    }
    public void Insert_user_newbie_attendance()
    {
        Debug.Log("Insert_user_new_attendance");
        Param param = new Param();
        param.Add("newbie_attendance_reward", "0,0,0,0,0,0,0");
        param.Add("newbie_attendance_time", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("newbie_attendance_count", "0");
        param.Add("newbie_attendance_check", "0");
        param.Add("newbie_attendance_puppy_reward_check", "0,0");
        param.Add("newbie_attendance_pet_reward_check", "0,0");

        BackendReturnObject insert = Backend.GameData.Insert(user_newbie_attendance_table, param);
        Debug.Log("Insert_user_newbie_attendance" + insert.GetStatusCode());
        if (insert.IsSuccess())
        {
            Get_user_newbie_attendance_info();
            Debug.Log("------------------------Insert_user_newbie_attendance success");
        }
    }
    public void Insert_user_attendance_package()
    {
        Debug.Log("Insert_user_attendance_package");
        Param param = new Param();
        param.Add("nomal_attendance_package_reward", "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
        param.Add("nomal_attendance_package_time", "0");
        param.Add("nomal_attendance_package_reward_count", "0");
        param.Add("nomal_attendance_package_count", "0");
        param.Add("nomal_attendance_package_check", "0");

        param.Add("premium_attendance_package_reward", "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
        param.Add("premium_attendance_package_time", "0");
        param.Add("premium_attendance_package_reward_count", "0");
        param.Add("premium_attendance_package_count", "0");
        param.Add("premium_attendance_package_check", "0");

        BackendReturnObject insert = Backend.GameData.Insert(user_attendance_package_table, param);
        Debug.Log("Insert_user_attendance_package_table" + insert.GetStatusCode());
        if (insert.IsSuccess())
        {
            Get_user_attendance_package_info();
            Debug.Log("------------------------Insert_user_attendance_package success");
        }
    }

    public void Insert_user_event_info()
    {
        Debug.Log("Insert_user_event_info");
        Param param = new Param();
        param.Add("event_monster_point", "0");
        param.Add("event_monster_total", "8600");

        param.Add("event_monster_check", "0,0,0,0,0,0,0");  // 포인트 교환 횟수
        param.Add("event_monster_limit", "50,50,10,20,15,10,5");  // 포인트 교환 제한
        param.Add("event_monster_price", "10,10,50,30,100,250,500");  // 포인트 교환 가격

        param.Add("event_package_check", "0,0,0,0,0"); // 패키지 구매 채크
        param.Add("event_package_limit", "5,5,5,5,5"); // 패키지 구매 제한

        BackendReturnObject insert = Backend.GameData.Insert("user_event_point", param);
        Debug.Log("Insert_user_year_info" + insert.GetStatusCode());
        if (insert.IsSuccess())
        {
            Get_user_event_info();
            Debug.Log("------------------------Insert_user_event_info success");
        }
    }

    public void Insert_user_event_pay_info()
    {
        Debug.Log("Insert_user_event_pay_info");
        Param param = new Param();
        param.Add("user_pay", "0");
        param.Add("event_reset_check", DateTime.Parse(ServerManager.Instance.Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).ToString("MM/yyyy"));
        param.Add("user_pay_reward_check", "0,0,0,0,0,0,0,0,0");  // 포인트 교환 횟수


        BackendReturnObject insert = Backend.GameData.Insert("user_event_pay", param);
        Debug.Log("Insert_user_event_pay_info" + insert.GetStatusCode());
        if (insert.IsSuccess())
        {
            Get_user_event_pay_info();
            Debug.Log("------------------------Insert_user_event_pay_info success");
        }
    }
    public void Insert_user_event_playTime_info()
    {
        Debug.Log("Insert_user_event_playTime_info");
        Param param = new Param();
        param.Add("user_playTime", "0");

        param.Add("user_playTime_reward_check", "0,0,0,0");  // 포인트 교환 횟수


        BackendReturnObject insert = Backend.GameData.Insert("user_event_playTime", param);
        Debug.Log("Insert_user_event_playTime_info" + insert.GetStatusCode());
        if (insert.IsSuccess())
        {
            Get_user_event_playTime_info();
            Debug.Log("------------------------Insert_user_event_playTime_info success");
        }
    }
    public void Insert_user_event_mission_info()
    {
        Debug.Log("Insert_user_event_mission_info");
        Param param = new Param();
        //모든 미션, 매일 로그인, 플레이 시간, 몬스터 처치, 뽑기, 환생, 합성, 공격력 간식, 공격속도 간식, PVP, 룬 던전, 보스전
        param.Add("event_mission_value", "0,0,0,0,0,0,0,0,0,0,0,0");
        param.Add("event_mission_check", "0,0,0,0,0,0,0,0,0,0,0,0");
        param.Add("event_monster_target", "11,1,30,10000,10,30,1,10,10,10,20,1");  // 미션 타겟
        param.Add("event_monster_reward", "100,50,50,0,0,0,0,0,0,0,0,0");  // 미트 보상

        // 패키지
        param.Add("event_mission_package_check", "0,0,0,0,0");
        param.Add("event_mission_package_limit", "5,5,5,5,5");

        BackendReturnObject insert = Backend.GameData.Insert("user_event_mission", param);
        Debug.Log("Insert_user_event_mission" + insert.GetStatusCode());
        if (insert.IsSuccess())
        {
            Get_user_event_info();
            Debug.Log("------------------------Insert_user_event_mission success");
        }
    }
    //1주년 테이블 삽입
    public void Insert_user_event_1st_anniversary_info()
    {
        Debug.LogWarning("Insert_user_event_1st_anniversary_info");
        Param param = new Param();
        param.Add("event_1st_anniversary_value", "0,0,0,0,0,0,0,0,0"); //유저 미션 값
        param.Add("event_1st_anniversary_check", "0,0,0,0,0,0,0,0,0"); //유저 미션 보상 수령 여부
        param.Add("event_1st_anniversary_bingo_check", "0,0,0,0,0,0,0,0,0"); //유저 미션 빙고 보상 수령 여부
        //PVP참여, 광고시청, 룬던전입장, 보스전, 환생, 합성, 몬스터처치, 누적출석, 누적출석
        param.Add("event_1st_anniversary_target", "18,50,100,18,1000,18,150000,5,7");  // 미션 타겟
        //A개,S펫,A개,A펫,날개5,공격력100,공속100,A펫,A개
        param.Add("event_1st_anniversary_reward", "1,1,1,1,1,10,10,1,1");  // 미션 클리어 보상
        param.Add("event_1st_anniversary_bingo_reward", "50,50,50,50,50,50,50,50,100");  // 빙고 클리어 보상 (s신수, s댕댕이)

        //출석 체크 
        param.Add("anniversary_attendance_reward", "0,0,0,0,0,0,0,0,0,0,0,0,0,0"); //출첵 보상 받앗는지 여부
        param.Add("anniversary_attendance_time", Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue())));
        param.Add("anniversary_attendance_count", "0");
        param.Add("anniversary_attendance_check", "0");

        BackendReturnObject insert = Backend.GameData.Insert("Private_table_0", param);
        Debug.Log("Insert_user_event_1st_anniversary_info" + insert.GetStatusCode());
        if (insert.IsSuccess())
        {
            Get_user_event_info();
            Debug.Log("------------------------Insert_user_event_1st_anniversary_info success");
        }
    }


    public void Insert_boss_info()
    {
        Debug.Log("Insert_user_boss");
        Param param = new Param();
        param.Add("boss_grade", "0");
        param.Add("boss_life", "0");
        param.Add("boss_damage", "0");
        param.Add("boss_attack_speed", "0");


        BackendReturnObject insert = Backend.GameData.Insert(boss_info, param);
        Debug.Log("Insert_user_boss" + insert.GetStatusCode());
        if (insert.IsSuccess())
        {
            Get_ranking_reward();
            Debug.Log("------------------------Insert_boss_info success");
        }
    }
    public void Insert_new_boss_info()
    {
        Debug.Log("Insert_new_boss_info");
        Param param = new Param();
        param.Add("boss_grade", "0");
        param.Add("boss_life", "0");
        param.Add("boss_damage", "0");
        param.Add("boss_attack_speed", "0");


        BackendReturnObject insert = Backend.GameData.Insert(new_boss_info, param);
        Debug.Log("Insert_user_boss" + insert.GetStatusCode());
        if (insert.IsSuccess())
        {
            //Get_ranking_reward();
            Debug.Log("------------------------Insert_boss_info success");
        }
    }

    public void Insert_user_rune_info()
    {
        Debug.Log("Insert_user_rune_info");
        Param param = new Param();
        param.Add("rune_level", "0,0,0,0"); // 
        param.Add("rune_stock", "0,0,0,0"); // 

        param.Add("rune_ad_time", "0"); // 
        param.Add("rune_stage", "0"); // 
        param.Add("rune_ticket", "10"); // 
        param.Add("rune_time", "0"); // 


        BackendReturnObject insert = Backend.GameData.Insert("user_rune_info", param);
        Debug.Log("user_rune_info" + insert.GetStatusCode());
        if (insert.IsSuccess())
        {
            Get_user_rune_info();
            Debug.Log("------------------------user_rune_info success");
        }
    }

    public void Insert_user_pet_info()
    {
        Debug.Log("Insert_user_pet_info");
        Param param = new Param();
        param.Add("user_pet", "0,0,0,0,0,0,0,0,0,0/0,0,0,0,0,0,0,0,0,0/0,0,0,0,0,0,0,0,0,0/0,0,0,0,0,0,0,0,0,0"); //펫 
        param.Add("select_pet_index", "0"); // 출석 보상 개수
        param.Add("select_pet_type", "0");
        param.Add("select_pet_isAwakening", "0");
        param.Add("pet_active", "0"); // 교환 개수 제한
        param.Add("pet_awakening_value", "0,0,0,0"); // 각성

        BackendReturnObject insert = Backend.GameData.Insert("user_pet_info", param);
        Debug.Log("user_pet_info" + insert.GetStatusCode());
        if (insert.IsSuccess())
        {
            Get_user_pet_info();
            Debug.Log("------------------------user_rune_info success");
        }
    }

    public void Update_user_pvp()
    {
        Debug.Log("-Update_user_pvp-");
        //Debug.Log("pvp 유저 데이터 업데이트");
        DBManager.Instance.pvp_user_info.nick_name = DBManager.Instance.user_info.nickname;
        DBManager.Instance.pvp_user_info.unit_index = GameManager.Instance.player_info.select_unit_index;
        DBManager.Instance.pvp_user_info.attack_damage = GameManager.Instance.player_info.player_attack_damage;
        DBManager.Instance.pvp_user_info.attack_speed = GameManager.Instance.player_info.player_attack_speed;
        DBManager.Instance.pvp_user_info.critical_damage = GameManager.Instance.player_info.player_critical_damage;
        DBManager.Instance.pvp_user_info.critical_percent = GameManager.Instance.player_info.player_critical_percent;
        DBManager.Instance.pvp_user_info.life = GameManager.Instance.player_info.player_life;
        //Debug.Log("보내기전 확인 " + GameManager.Instance.player_info.player_attack_speed);
        Param param = new Param();
        param.Add("unit", GameManager.Instance.player_info.select_unit_index);
        param.Add("attack_damage", GameManager.Instance.player_info.player_attack_damage);
        param.Add("nickname", DBManager.Instance.user_info.nickname.ToString());
        param.Add("attack_speed", Math.Truncate(GameManager.Instance.player_info.player_attack_speed * 100) / 100);
        //param.Add("attack_speed", GameManager.Instance.player_info.player_attack_speed.ToString("F1"));
        param.Add("critical_damage", GameManager.Instance.player_info.player_critical_damage);
        param.Add("critical_percent", Math.Truncate(GameManager.Instance.player_info.player_critical_percent * 100) / 100);
        //param.Add("critical_percent",GameManager.Instance.player_info.player_critical_percent.ToString("F1"));
        param.Add("life", GameManager.Instance.player_info.player_life);
        param.Add("best_stage", DBManager.Instance.user_info.best_stage.GetDecrypted());
        param.Add("pet_active", DBManager.Instance.user_pet_info.pet_active.GetDecrypted());
        param.Add("pet_type", DBManager.Instance.user_pet_info.select_pet_type.GetDecrypted());
        param.Add("pet_index", DBManager.Instance.user_pet_info.select_pet_index.GetDecrypted());
        //Debug.Log(param.GetJson());

        Backend.GameData.Update(user_pvp_table, DBManager.Instance.pvp_user_info.indate, param, (callback) =>
        {
            error_type = 15;
            server_loading_time = 0;
            server_loading = true;
            Debug.Log("유저 개인 pvp 정보 업데이트" + callback.GetStatusCode());
            Debug.Log("유저 개인 pvp 정보 업데이트" + callback.GetMessage());
            //Debug.Log(param.GetJson());
            if (callback.GetStatusCode() == "204")
            {
                if (UIManager.Instance.pvp_panel.pvp_time == true)
                {
                    if (UIManager.Instance.pvp_panel.is_already_enemy_match == false)
                    {
                        Get_user_pvp_info();
                    }
                    else
                    {
                        UIManager.Instance.pvp_panel.pvp_info_panel_setting();
                    }
                }
                else
                {
                    server_loading = false;
                }
            }
            else
            {
                server_loading = false;
                error_type = 15;
                Server_connect_check();
            }
        });
    }

    public void GetUserInfoByNickName(string nickname)
    {
        /*
        var enemyInfo = Backend.GameData.GetMyData("user_info", enemyIndate);
        if (enemyInfo.IsSuccess())
        {
            if (enemyInfo.GetReturnValuetoJSON()["rows"].Count <= 0)
            {
                Debug.LogWarning("-----------row가 0-------------" + enemyInfo.GetStatusCode());
            }
            else
            {
                var data = enemyInfo.Rows()[0]["updatedAt"]["S"].ToString();
                Debug.LogWarning("-----------asdfasdfsdfasd-------------");
            }
        }
        else
        {
            Debug.LogWarning("-----------정보 얻어오기 실패-------------" + enemyInfo.GetStatusCode());
        }
        */
    }

    void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit IN");
        // 큐에 처리되지 않는 요청이 남아있는 경우 대기하고 싶은 경우
        // 큐에 몇 개의 함수가 남아있는지 체크

        // 로컬 푸시
        /*
#if UNITY_ANDROID
        if (isPush) {
            string local_push_newbie= "신규 유저 출석 알림";
            string local_push_newbie_content = "신규 유저 출석 보상을 수령하세요!";

            if (PlayerPrefs.GetInt("NewbieattendanceCount") < 7)
            {
                string time = PlayerPrefs.GetString("firstTime");
                DateTime first_in_play_time = DateTime.Parse(time);

                if(PlayerPrefs.GetInt("IsNotifyCount") == 0)
                {
                    if(PlayerPrefs.GetInt("NewbieattendanceCount") == 1)
                    {
                        Debug.Log("NewbieattendanceCount = 1");
                        NotificationManager.CancelAll();
                        //DateTime newbie_day_1 = first_in_play_time.AddDays(1);
                        DateTime newbie_day_1 = first_in_play_time.AddMinutes(1);
                        TimeSpan Day1_push = newbie_day_1 - DateTime.Now;
                        NotificationManager.SendWithAppIcon(Day1_push, local_push_newbie, local_push_newbie_content, Color.black, NotificationIcon.Bell);

                        //DateTime newbie_day_3 = first_in_play_time.AddDays(3);
                        DateTime newbie_day_3 = first_in_play_time.AddMinutes(3);
                        TimeSpan Day3_push = newbie_day_3 - DateTime.Now;
                        NotificationManager.SendWithAppIcon(Day3_push, local_push_newbie, local_push_newbie_content, Color.black, NotificationIcon.Bell);

                        //DateTime newbie_day_7 = first_in_play_time.AddDays(7);
                        DateTime newbie_day_7 = first_in_play_time.AddMinutes(5);
                        TimeSpan Day7_push = newbie_day_7 - DateTime.Now;
                        NotificationManager.SendWithAppIcon(Day7_push, local_push_newbie, local_push_newbie_content, Color.black, NotificationIcon.Bell);
                    }

                    if(PlayerPrefs.GetInt("NewbieattendanceCount") == 2)
                    {
                        Debug.Log("NewbieattendanceCount = 2");
                        NotificationManager.CancelAll();
                        //DateTime newbie_day_3 = first_in_play_time.AddDays(3);
                        DateTime newbie_day_3 = first_in_play_time.AddMinutes(1);
                        TimeSpan Day3_push = newbie_day_3 - DateTime.Now;
                        NotificationManager.SendWithAppIcon(Day3_push, local_push_newbie, local_push_newbie_content, Color.black, NotificationIcon.Bell);

                        //DateTime newbie_day_7 = first_in_play_time.AddDays(7);
                        DateTime newbie_day_7 = first_in_play_time.AddMinutes(3);
                        TimeSpan Day7_push = newbie_day_7 - DateTime.Now;
                        NotificationManager.SendWithAppIcon(Day7_push, local_push_newbie, local_push_newbie_content, Color.black, NotificationIcon.Bell);
                    }

                    if(PlayerPrefs.GetInt("NewbieattendanceCount") >= 3 && PlayerPrefs.GetInt("NewbieattendanceCount") < 7)
                    {
                        Debug.Log("NewbieattendanceCount = 3~6");
                        NotificationManager.CancelAll();
                        //DateTime newbie_day_7 = first_in_play_time.AddDays(7);
                        DateTime newbie_day_7 = first_in_play_time.AddMinutes(1);
                        TimeSpan Day7_push = newbie_day_7 - DateTime.Now;
                        NotificationManager.SendWithAppIcon(Day7_push, local_push_newbie, local_push_newbie_content, Color.black, NotificationIcon.Bell);
                    }

                    if(PlayerPrefs.GetInt("NewbieattendanceCount") >= 7)
                    {
                        Debug.Log("NewbieattendanceCount = 7~");
                        NotificationManager.CancelAll();
                    }

                    PlayerPrefs.SetInt("isNotifyCount", 1);
                }
            }
            else
            {
                Debug.Log("test");
            }
        }
#endif
        */
        DBManager.Instance.DB_Update_user_info();
        DBManager.Instance.DB_Update_user_pet_info();
        DBManager.Instance.DB_Update_user_rune_info();
        DBManager.Instance.DB_Update_user_event_playTime_info();
    }

    public void Network_check()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable) // 연결 안되어 있음
        {
            Time.timeScale = 0;
            error_type = 27;
            network_check = false;
            Server_connect_check();
        }
        else // 연결 되어있음
        {
            Time.timeScale = 1;
            network_check = true;
            if (list_check_data_tampering.Count > 0)
            {
                for (int i = 0; i < list_check_data_tampering.Count; i++)
                {
                    send_log_check_data_tampering(list_check_data_tampering[i]);
                }
                list_check_data_tampering.Clear();
            }
        }
    }

    //네트워크 끊김 상태에서의 데이터 변조 체크
    public bool CheckDataTampering()
    {
        Network_check();
        if (network_check == false)
        {
            return true;

        }
        return false;
    }

    public void save_check_data_tampering_log(string type, string before_value)
    {

        Param param = new Param();
        param.Add("현재시간", System.DateTime.Now);
        param.Add("닉네임", DBManager.Instance.user_info.nickname);
        param.Add("보상 종류", type);
        param.Add("변조 시도 전", before_value);

        list_check_data_tampering.Add(param);
    }

    public void send_log_check_data_tampering(Param param)
    {
        Debug.LogWarning("send_log_check_data_tampering in");

        Backend.GameLog.InsertLog("check_data_tampering", param, (callback) =>
        {
            // 이후 처리
        });
    }

    public void attendance_package_test()
    {
        for (int i = 0; i < DBManager.Instance.user_attendance_package.nomal_attendance_package_reward.Count; i++)
        {
            DBManager.Instance.user_attendance_package.nomal_attendance_package_reward[i] = 0;
        }
        DBManager.Instance.user_attendance_package.nomal_attendance_package_time = System.DateTime.Parse(ServerManager.Instance.Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).AddDays(1).ToString("MM/dd/yyyy H:mm:ss");
        DBManager.Instance.user_attendance_package.nomal_attendance_package_count = 1;
        DBManager.Instance.user_attendance_package.nomal_attendance_package_check = 1;
        DBManager.Instance.user_attendance_package.nomal_attendance_package_reward_count = 0;

        //ServerManager.Instance.send_log_auto_package(product_id, args.purchasedProduct.receipt.ToString(), DBManager.Instance.user_info.auto_again_active.ToString(), DBManager.Instance.user_info.auto_again_time);
        UIManager.Instance.shop_panel.Package_panel_setting();
        DBManager.Instance.DB_Update_user_attendance_package_info();
    }
    public void attendance_package_text_1()
    {
        for (int i = 0; i < DBManager.Instance.user_attendance_package.premium_attendance_package_reward.Count; i++)
        {
            DBManager.Instance.user_attendance_package.premium_attendance_package_reward[i] = 0;
        }
        DBManager.Instance.user_attendance_package.premium_attendance_package_time = System.DateTime.Parse(ServerManager.Instance.Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).AddDays(1).ToString("MM/dd/yyyy H:mm:ss");
        DBManager.Instance.user_attendance_package.premium_attendance_package_count = 1;
        DBManager.Instance.user_attendance_package.premium_attendance_package_check = 1;
        DBManager.Instance.user_attendance_package.premium_attendance_package_reward_count = 0;

        //ServerManager.Instance.send_log_auto_package(product_id, args.purchasedProduct.receipt.ToString(), DBManager.Instance.user_info.auto_again_active.ToString(), DBManager.Instance.user_info.auto_again_time);

        UIManager.Instance.shop_panel.Package_panel_setting();
        DBManager.Instance.DB_Update_user_attendance_package_info();
    }

    public void Set_push_on_off(int type)
    {
        Debug.Log("----- Set_push_on_off IN -----");
        Debug.Log("Set_push_on_off() - push : " + PlayerPrefs.GetInt("push"));
        if (type == 0)
        {
            isPush = false;
        }
        else if (type == 1)
        {
            isPush = true;
        }
        Set_push();
    }

    public void Set_push()
    {
        Debug.Log("----- Set_push -----");
        // Debug.Log("디바이스 토큰 : " + Backend.Android.GetDeviceToken());
        if (isPush)
        {   // 푸시설정 ON
            /*
            #if UNITY_ANDROID && !UNITY_EDITOR
                        Backend.Android.PutDeviceToken();
            #elif UNITY_IOS && !UNITY_EDITOR
                        Backend.iOS.PutDeviceToken(isDevelopment.iosDev); // 개발자 버전에서 사용
                        //Backend.iOS.PutDeviceToken(isDevelopment.iosProd); // 실제 배포용
            #else
            #endif*/

#if UNITY_ANDROID && !UNITY_EDITOR
                        Debug.Log("===isPush if문 - 안드로이드===");

                        Backend.Android.PutDeviceToken(Backend.Android.GetDeviceToken(), (callback) => 
                        {
                            Debug.Log("===PutDeviceToken===");
                            if(callback.GetStatusCode() == "204")
                            {
                                Debug.Log("푸시 알림 설정 성공");
                                Debug.Log(callback.IsSuccess());
                                Debug.Log(callback.ToString());
                            }else
                            {
                                Debug.Log("푸시 알림 설정 실패");
                                Debug.Log(callback.GetErrorCode());
                            }
                        });
#elif UNITY_IOS && !UNITY_EDITOR
                        //Backend.iOS.PutDeviceToken(isDevelopment.iosDev); // 개발자 버전에서 사용
                        //Backend.iOS.PutDeviceToken(isDevelopment.iosProd); // 실제 배포용
#else
#endif
        }
        else
        {
            // 푸시설정 OFF
            /*
            #if UNITY_ANDROID && !UNITY_EDITOR
                        Backend.Android.DeleteDeviceToken();
            #elif UNITY_IOS && !UNITY_EDITOR
                        Backend.iOS.DeleteDeviceToken();
            #else
            #endif*/

#if UNITY_ANDROID && !UNITY_EDITOR
                        Backend.Android.DeleteDeviceToken((callback) =>
                        {
                            if (callback.GetStatusCode() == "204")
                            {
                                Debug.Log("푸시 알림 삭제 성공");
                                Debug.Log(callback.IsSuccess());
                                Debug.Log(callback.ToString());
                            }else
                            {
                                Debug.Log("푸시 알림 삭제 실패");
                                Debug.Log(callback.GetErrorCode());
                            }
                        });
#elif UNITY_IOS && !UNITY_EDITOR
                       // Backend.iOS.DeleteDeviceToken();
#else
#endif
        }
    }

    // 랭킹 업데이트, 업데이트 후 랭킹 점수 가져오기.
    public void Get_ranking_match_result()
    {
        Debug.Log("----- Get_ranking_match_result IN -----");
        server_loading_time = 0;
        server_loading = true;

        if (gm || DBManager.Instance.user_ranking_match_info.ranking_match_point == 0)
        {
            return;
            Param param = new Param();
            param.Add("ranking_match_point", 0);

            Backend.URank.User.UpdateUserScore(ranking_match_ranking, ranking_match_table, DBManager.Instance.user_ranking_match_info.InDate, param, callback1 =>
            {
                if (callback1.GetStatusCode() == "204")
                {
                    Debug.Log("-- 랭킹전 랭킹 갱신 성공 --");
                    Backend.URank.User.GetMyRank(ranking_match_ranking, callback2 =>
                    {
                        if (callback2.GetStatusCode() == "200")
                        {
                            Debug.Log("-- 자기 랭킹 가져오기 성공 --");
                            DBManager.Instance.user_ranking_match_info.ranking_match_current_rank = int.Parse(callback2.GetReturnValuetoJSON()["rows"][0]["rank"]["N"].ToString());
                            //UIManager.Instance.ranking_panel.Ranking_panel_ranking_match_update(callback2.GetReturnValuetoJSON());
                            server_loading = false;
                        }
                        else
                        {
                            Debug.Log("-- 자기 랭킹 가져오기 실패 --");
                            error_type = 48;
                            send_log_ranking_match_error(error_type, callback1.GetStatusCode(), callback1.GetMessage());
                        }
                    });
                }
                else
                {
                    Debug.Log("-- 랭킹전 랭킹 갱신 실패 --");
                    error_type = 48;
                    Server_connect_check();
                    send_log_ranking_match_error(error_type, callback1.GetStatusCode(), callback1.GetMessage());
                }
            });
        }
        else
        {
            Param param = new Param();
            param.Add("ranking_match_point", DBManager.Instance.user_ranking_match_info.ranking_match_point.GetDecrypted());

            Backend.URank.User.UpdateUserScore(ranking_match_ranking, ranking_match_table, DBManager.Instance.user_ranking_match_info.InDate, param, callback1 =>
            {
                if (callback1.GetStatusCode() == "204")
                {
                    Debug.Log("-- 랭킹전 랭킹 갱신 성공 --");
                    Backend.URank.User.GetMyRank(ranking_match_ranking, callback2 =>
                    {
                        if (callback2.GetStatusCode() == "200")
                        {
                            Debug.Log("-- 자기 랭킹 가져오기 성공 --");
                            DBManager.Instance.user_ranking_match_info.ranking_match_current_rank = int.Parse(callback2.GetReturnValuetoJSON()["rows"][0]["rank"]["N"].ToString());
                            server_loading = false;
                        }
                        else
                        {
                            Debug.Log("-- 자기 랭킹 가져오기 실패 --");
                            error_type = 48;
                            Network_check();
                            send_log_ranking_match_error(error_type, callback1.GetStatusCode(), callback1.GetMessage());
                        }
                    });
                }
                else
                {
                    Debug.Log("-- 랭킹전 랭킹 갱신 실패 --");
                    error_type = 48;
                    Network_check();
                    send_log_ranking_match_error(error_type, callback1.GetStatusCode(), callback1.GetMessage());
                }
            });
        }
    }

    // 랭킹이 없으면 랭킹점수 기본값 할당후 서버로 저장
    /*public void ranking_match_rank_check()
    {
        Debug.Log("----- pvp_ranking_check IN -----");
        Backend.URank.User.GetMyRank(ranking_match_ranking, 0, callback =>
        {
            if (callback.GetStatusCode() == "404") // 랭킹에 없음
            {
                Get_ranking_match_result();
            }
        });
    }*/

    // 랭킹전 초기값 인서트
    public void Insert_ranking_match_info()
    {
        Debug.Log("----- Insert_ranking_match_info -----");
        Param param = new Param();
        param.Add("current_stage", 0);
        param.Add("user_nickname", DBManager.Instance.user_info.nickname);
        //param.Add("ranking_match_end_time", "2022-02-07T05:00:00.000Z");
        param.Add("ranking_match_end_time", "2022-02-13T18:59:59.999Z"); // 2월 14일 새벽 4시 초기화.
        param.Add("ranking_match_point", 0);
        param.Add("ranking_match_ticket", 140);
        param.Add("ranking_match_ticket_use_time", "0");
        param.Add("ranking_match_buy_ticket", 0);
        param.Add("stage_inDate", "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
        param.Add("stage_reward_check", "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
        param.Add("stage_nickname", "0");
        param.Add("stage_totalplaytime", 0);
        param.Add("stage_dog_index", 0);
        param.Add("stage_attack_damage", 0);
        param.Add("stage_attack_speed", 0);
        param.Add("stage_critical_damage", 0);
        param.Add("stage_critical_percent", 0);
        param.Add("stage_life", 0);
        param.Add("stage_current_remaining_life", 0);
        param.Add("stage_pet_index", 0);
        param.Add("stage_pet_type", 0);
        param.Add("is_get_stage", 0);

        BackendReturnObject insert = Backend.GameData.Insert(ranking_match_table, param);
        if (insert.IsSuccess() == true)
        {
            Debug.Log("-- 랭킹전 정보 삽입 성공 --");
            Get_user_ranking_match_info();
        }
    }

    // 랭킹전 끝나는 시간 가져오기
    public void Get_ranking_match_end_time()
    {
        Debug.Log("Get_ranking_match_end_time in");
        UIManager.Instance.ranking_match_panel.ranking_match_end_time = DBManager.Instance.user_ranking_match_info.ranking_match_end_time;
    }

    // 랭킹전 유저 정보 가져오기
    public void Get_user_ranking_match_info()
    {
        Debug.Log("----- Get_user_ranking_match_info IN -----");
        Backend.GameData.GetMyData(ranking_match_table, new Where(), callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                if (callback.GetReturnValuetoJSON()["rows"].Count <= 0)
                {
                    Debug.Log("-- 랭킹전 정보 삽입 --");
                    Insert_ranking_match_info();
                }
                else
                {
                    Debug.Log("-- 랭킹전 정보 가져오기 성공 --");
                    DBManager.Instance.DB_insert_user_ranking_match_info(callback.GetReturnValuetoJSON());

                    //1주일에 한 번 집계 후 최초 접속 시 스테이지 정보 갱신 ->트루로 들어올 경우 랭킹매치엔드 타임 갱신해서 1회 갱신하면 다시 못 들어옴
                    if (DateTime.Parse(DBManager.Instance.user_ranking_match_info.ranking_match_end_time) <= server_time)
                    {
                        Debug.LogWarning("랭킹전 집계시간 이후 최초 접속으로 스테이지 정보 초기화");
                        Get_stage_user();
                        //Get_stage_user의 indate list와 DB_insert_ranking_match_stage_info의 rank_indate_list가 다를 수 있으므로 비교해서 잘못된 데이터 색출
                        //DBManager.Instance.user_ranking_match_info.rank_InDate

                    }
                    Get_ranking_match_stage_info();
                }
            }
            else
            {
                Debug.Log("-- 랭킹전 정보 가져오기 실패 --");
                error_type = 44;
                Network_check();
                send_log_ranking_match_error(error_type, callback.GetStatusCode(), callback.GetMessage());
            }
        });
    }

    //1~100층 유저 인데이트 가져오기
    //1주일 마다 가져오기
    public void Get_stage_user()
    {
        Debug.Log("----- Get_stage_user IN -----");
        Debug.Log("endtime :" + DateTime.Parse("2022-02-06T18:59:59.999Z"));
        Debug.LogWarning(DBManager.Instance.user_ranking_match_info.is_get_stage);

        //if (DateTime.Parse(DBManager.Instance.user_ranking_match_info.ranking_match_end_time) < server_time)
        {
            BackendReturnObject power_rank = Backend.RTRank.GetRTRankByUuid(power_ranking, 100);

            if (power_rank.IsSuccess() == true)
            {
                Debug.Log("-- 1위 ~ 100위 정보 가져오기 성공 --");
                DBManager.Instance.DB_insert_rank_info(power_rank.GetReturnValuetoJSON(), "Power");
            }
            else
            {
                Debug.Log("-- 1위 ~ 100위 정보 가져오기 실패 --");
                error_type = 47;
                Network_check();
                send_log_ranking_match_error(error_type, power_rank.GetStatusCode(), power_rank.GetMessage());
            }
        }

        Debug.Log("----- Get_stage_user OUT -----");

        /*
        if (ranking_match_time == true) // 1회차
        {
            Debug.Log("Power");

            if (DBManager.Instance.check_rank_inDate == true)
            {
                BackendReturnObject power_rank = Backend.RTRank.GetRTRankByUuid(power_ranking, 100);
                DBManager.Instance.DB_insert_rank_info(power_rank.GetReturnValuetoJSON(), "Power");
            }
        }
        else // 1회차 이후
        {
            Debug.Log("Ranking_match");
            
            if (DBManager.Instance.check_rank_inDate == true)
            {
                BackendReturnObject ranking_match_rank = Backend.URank.User.GetRankList(ranking_match_ranking, 100);
                DBManager.Instance.DB_insert_rank_info(ranking_match_rank.GetReturnValuetoJSON(), "Ranking match");
            }
        }
        */
    }

    // 랭킹전 적 정보 가져오기
    // 스테이지 클리어 할 때, 집계시간 이후 접속 할 때
    // 현재 스테이지만 불러오기
    public void Get_ranking_match_stage_info()
    {
        Debug.Log("----- Get_ranking_match_stage_info IN -----");

        int stage_loding_limit = 99 - DBManager.Instance.user_ranking_match_info.current_stage;
        if (stage_loding_limit >= 0)
        {
            Debug.Log("스테이지가 100보다 작을 때");
            Where where = new Where();
            where.Equal("owner_inDate", DBManager.Instance.user_ranking_match_info.rank_InDate[stage_loding_limit]);

            BackendReturnObject user_pvp_info = Backend.GameData.Get(user_pvp_table, where);
            if (user_pvp_info.IsSuccess() == true)
            {
                if (user_pvp_info.GetReturnValuetoJSON()["rows"].Count <= 0)
                {
                    Debug.Log("-- 스테이지 세부 정보 없음 --");
                }
                else
                {
                    Debug.Log("-- 스테이지 세부 정보 가져오기 성공 --");
                    DBManager.Instance.DB_insert_ranking_match_stage_info(user_pvp_info.GetReturnValuetoJSON(), stage_loding_limit);
                }
            }
            else
            {
                Debug.Log("-- 스테이지 세부 정보 가져오기 실패 --");
                error_type = 45;
                Network_check();
                send_log_ranking_match_error(error_type, user_pvp_info.GetStatusCode(), user_pvp_info.GetMessage());
            }
        }
        else
        {
            Debug.Log("스테이지가 100보다 클 때");
            Where where = new Where();
            where.Equal("owner_inDate", DBManager.Instance.user_ranking_match_info.rank_InDate[0]);

            BackendReturnObject user_pvp_info = Backend.GameData.Get(user_pvp_table, where);
            if (user_pvp_info.IsSuccess() == true)
            {
                if (user_pvp_info.GetReturnValuetoJSON()["rows"].Count <= 0)
                {
                    Debug.Log("-- 스테이지 세부 정보 없음 --");
                }
                else
                {
                    Debug.Log("-- 스테이지 세부 정보 가져오기 성공 --");
                    DBManager.Instance.DB_insert_ranking_match_stage_info(user_pvp_info.GetReturnValuetoJSON(), 0);
                }
            }
            else
            {
                Debug.Log("-- 스테이지 세부 정보 가져오기 실패 --");
                error_type = 45;
                Network_check();
                send_log_ranking_match_error(error_type, user_pvp_info.GetStatusCode(), user_pvp_info.GetMessage());
            }
        }
    }

    // 랭킹전 랭킹 가져오기
    public void Get_ranking_match_rank()
    {
        Debug.Log("----- Get_ranking_match_rank IN -----");
        Backend.URank.User.GetRankList(ranking_match_ranking, 100, callback =>
        {
            if (callback.GetStatusCode() == "200")
            {
                if (callback.GetReturnValuetoJSON()["rows"].Count <= 0)
                {
                    Debug.Log("-- 랭킹전 랭킹 데이터 없음 --");
                }
                else
                {
                    Debug.Log("-- 랭킹전 랭킹 조회 성공 --");
                    DBManager.Instance.DB_insert_ranking_match_user_rank(callback.GetReturnValuetoJSON());
                    Backend.URank.User.GetMyRank(ranking_match_ranking, callback1 =>
                    {
                        if (callback1.GetStatusCode() == "200")
                        {
                            DBManager.Instance.DB_insert_ranking_match_my_rank(callback1.GetReturnValuetoJSON());
                            UIManager.Instance.ranking_panel.Ranking_panel_ranking_match_update(1);
                        }
                        else
                        {
                            UIManager.Instance.ranking_panel.Ranking_panel_ranking_match_update(0);
                        }
                    });
                }
            }
            else
            {
                Debug.Log("-- 랭킹전 랭킹 조회 실패 --");
                error_type = 46;
                Network_check();
                send_log_ranking_match_error(error_type, callback.GetStatusCode(), callback.GetMessage());
            }
        });
    }

    public void send_log_ranking_match_play(int before_ticket, long before_point, int after_ticket, long after_point)
    {
        Debug.Log("send_log_ranking_match_play IN");

        Param param = new Param();

        param.Add("현재시간", DateTime.Now);
        param.Add("사용전 티켓 수", before_ticket);
        param.Add("사용후 티켓 수", after_ticket);
        param.Add("티켓 사용 전 랭킹 점수", before_point);
        param.Add("티켓 사용 후 랭킹 점수", after_point);

        Backend.GameLog.InsertLog("ranking_match_play", param, (callback) => { });
    }

    // 티켓 샀을 때 로그
    public void send_log_ranking_match_buy_ticket(int before_ticket, int after_ticket, long before_meet, long after_meet)
    {
        Debug.Log("send_log_pvp_buy_ticket IN");
        Param param = new Param();
        param.Add("현재시간", DateTime.Now);
        param.Add("받기전 티켓", before_ticket);
        param.Add("받은뒤 티켓", after_ticket);
        param.Add("변경전 미트", before_meet);
        param.Add("변경후 미트", after_meet);

        Backend.GameLog.InsertLog("ranking_match_buy_ticket", param, (callback) => { });
    }

    public void send_log_pet_awakening(int pet_type, int before_pet_stock, int after_pet_stock, int awakening_level)
    {
        Debug.Log("send_log_pet_awakening");
        Param param = new Param();
        param.Add("현재시간", server_time.ToString("MM/dd/yyyy H:mm:ss"));
        param.Add("펫 종류", pet_type);
        param.Add("각성 전 펫 갯수", before_pet_stock);
        param.Add("각성 후 펫 갯수", after_pet_stock);
        param.Add("각성 후 단계", awakening_level);

        Backend.GameLog.InsertLog("pet_awakening", param, (callback) => { });
    }


    // 랭킹전 오류 났을 때 오류 로그
    public void send_log_ranking_match_error(int error_type, string error_number, string error_message)
    {
        Debug.Log("send_log_ranking_match_error IN");

        Param param = new Param();
        param.Add("현재 시간", DateTime.Now);
        param.Add("오류 타입", error_type);
        param.Add("오류 코드", error_number);
        param.Add("오류 내용", error_message);

        Backend.GameLog.InsertLog("ranking_match_error", param, (callback) => { });
    }
}