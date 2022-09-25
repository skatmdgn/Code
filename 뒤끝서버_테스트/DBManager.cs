using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using LitJson;
using BackEnd;
using System;
using UnityEngine.SceneManagement;
using Firebase.DynamicLinks;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Linq;

//using Firebase.DynamicLinks;
[System.Serializable]
public class DBManager : MonoBehaviour
{
    public static DBManager Instance { private set; get; }

    private bool editor_check = false;

    public string google_store_url;
    public string apple_store_url;

    public float bgm_value;
    public float sfx_value;

    TextAsset textdata;

    public int terms;

    public List<List<string>> List_language = new List<List<string>>(); // 1. 한국어  // 2. 영어 // 3. 중국어 간체 // 4. 중국어 번체 // 5. 일본어
    public int language_index = 0;
    public List<string> gm = new List<string>();

    public List<Achievement_info> list_achievement_info = new List<Achievement_info>();
    public List<Post_info> list_post_info = new List<Post_info>();
    public List<Collection_info> list_collection_info = new List<Collection_info>();
    public List<Pet_collection_info> list_pet_collection_info = new List<Pet_collection_info>();
    public List<Unit_info> list_unit_info = new List<Unit_info>();
    public List<Boss_info> list_boss_info = new List<Boss_info>();
    public List<Upgrade_info> list_upgrade_info = new List<Upgrade_info>();
    public List<Quest_info> list_quest_info = new List<Quest_info>();
    public User_info user_info = new User_info();
    public User_boss_info user_boss_info = new User_boss_info();
    public User_rune_info user_rune_info = new User_rune_info();
    public User_pet_info user_pet_info = new User_pet_info();
    public Attendance_info attendance_info = new Attendance_info();
    public User_newbie_attendance_info user_newbie_attendance_info = new User_newbie_attendance_info();
    public User_attendance_package_info user_attendance_package = new User_attendance_package_info();
    public User_event_info user_event_info = new User_event_info();
    public User_event_pay_info user_event_pay_info = new User_event_pay_info();
    public User_event_playTime_info user_event_playTime_info = new User_event_playTime_info();
    public User_event_mission_info user_event_mission_info = new User_event_mission_info();
    public User_event_1st_anniversary_info user_event_1st_anniversary_info = new User_event_1st_anniversary_info();
    public User_year_info user_year_info = new User_year_info();
    public List<string> list_tip = new List<string>();

    // 게임 정보
    public List<List<int>> user_skill_value = new List<List<int>>();  // 유저 스킬 레벨 밸류
    //public List<string> user_skill_info;
    public List<ObscuredInt> tresure_box_price;
    public List<int> exp = new List<int>(); // 레벨업 경험치

    // 유저 랭킹
    public List<User_ranking> list_total_stage = new List<User_ranking>();
    public List<User_ranking> list_best_stage = new List<User_ranking>();
    public List<User_ranking> list_total_again = new List<User_ranking>();
    public List<User_ranking> list_pvp = new List<User_ranking>();
    public List<User_ranking> list_power = new List<User_ranking>();
    public List<User_ranking> list_ranking_match = new List<User_ranking>(); // 랭킹전 랭킹
    public User_ranking user_total_stage = new User_ranking();
    public User_ranking user_best_stage = new User_ranking();
    public User_ranking user_power = new User_ranking();
    public User_ranking user_pvp = new User_ranking();
    public User_ranking user_ranking_match = new User_ranking();

    //PVP 적 정보
    public bool ckeck_current_rank = true;
    public pvp_enemy pvp_enemy_info = new pvp_enemy();
    public pvp_user pvp_user_info = new pvp_user();

    public float user_mission_update_time = 0;
    //1주년 데이터 업데이트 주기
    public float user_1st_anniversary_update_time = 0;
    public float user_info_update_time = 0;
    public float check_enemy_update_time = 0;
    public bool pvp_setting = false;

    // 공지사항 이미지
    public List<Texture2D> list_notice_image = new List<Texture2D>();
    public List<string> list_notice_url = new List<string>();

    // 랭킹전
    public bool ranking_match_setting = false;
    public User_ranking_match_info user_ranking_match_info = new User_ranking_match_info();
    //public bool check_rank_inDate = false;

    public int retry = 0;

    [Serializable]
    public class User_ranking_match_info // 랭킹전 유저 정보
    {
        public string InDate; // 플레이어 InDate
        public string user_nickname; // 플레이어 닉네임
        public ObscuredInt current_stage; // 현재 진행 중인 스테이지
        public ObscuredInt ranking_match_ticket; // 랭킹전 티켓
        public ObscuredInt ranking_match_point; // 랭킹전 랭킹점수
        public ObscuredInt ranking_match_buy_ticket; // 랭킹전 티켓 구매 횟수 카운트
        public string ranking_match_end_time; // 랭킹전 끝나는 시간
        public string ranking_match_ticket_use_time; // 마지막으로 사용한 티켓
        public List<string> rank_InDate; // 랭킹 유저 InDate
        public List<ObscuredInt> stage_reward_check;
        public int ranking_match_current_rank;
        public int is_get_stage;
        public int active_auto_ranking_match;
        public int auto_ranking_match_stage_setting;

        // 스테이지 클리어할 때 다음 스테이지 정보 가져오기
        // 스테이지 관련 정보

        public string stage_nickname;
        public ObscuredInt stage_totalplaytime;
        public ObscuredInt stage_atteck_damage;
        public ObscuredFloat stage_atteck_speed;
        public ObscuredInt stage_critical_damage;
        public ObscuredFloat stage_critical_percent;
        public ObscuredLong stage_life;
        public ObscuredLong stage_current_remaining_life;
        public ObscuredInt stage_dog_index;
        public ObscuredLong stage_power;
        public ObscuredInt stage_pet_index;
        public ObscuredInt stage_pet_type;
    }

    [Serializable]
    public class Unit_info //유닛 정보 
    {
        public ObscuredInt unit_index;
        public ObscuredInt grade;
        public string name;
        public string info;
        public ObscuredInt attack_damage;    // 기본 공격력
        public ObscuredInt attack_speed;     // 기본 공격속도
        public ObscuredInt life;             // 기본 체력
        public ObscuredInt critical_damage;  // 기본 크리티컬 데미지
        public ObscuredFloat critical_percent; // 기본 크리티컬 확률
        public ObscuredInt ability_type;
        public ObscuredInt ability_value;
    }

    public List<int> grade_1_unit = new List<int>();
    public List<int> grade_2_unit = new List<int>();
    public List<int> grade_3_unit = new List<int>();
    public List<int> grade_4_unit = new List<int>();
    public List<int> grade_5_unit = new List<int>();
    public List<int> grade_6_unit = new List<int>();
    public List<int> grade_7_unit = new List<int>();
    public List<int> grade_8_unit = new List<int>();

    [Serializable]
    public class Collection_info // 컬랙션 정보
    {
        public int collection_index; // 컬렉션 번호

        public string collection_name;
        public string coolection_info;

        public string unit_1_index;
        public string unit_2_index;
        public string unit_3_index;
        public string unit_4_index;
        public string unit_5_index;

        public int ability_type;
        public int ability_value;
    }

    [Serializable]
    public class Pet_collection_info // 컬랙션 정보
    {
        public int collection_index; // 컬렉션 번호

        public string pet_1;
        public string pet_2;
        public string pet_3;
        public string pet_4;

        public string pet_grade;

        public int ability_type;
        public int ability_value;
    }


    [Serializable]
    public class User_event_info // 유저 이벤트 정보
    {
        public string inDate; // 

        public ObscuredInt event_monster_point; // 교환 포인트 개수  기본 0
        public ObscuredInt event_monster_total; // 총 포인트 획득량 기본 0 
        public List<ObscuredInt> event_monster_check = new List<ObscuredInt>(); // 포인트 교환 횟수  기본 0,0,0,0,0,0
        public List<ObscuredInt> event_monster_limit = new List<ObscuredInt>(); // 교환 환계점
        public List<ObscuredInt> event_monster_price = new List<ObscuredInt>(); // 교환 포인트 가격

        public List<ObscuredInt> event_package_check = new List<ObscuredInt>(); // 패키지 구매 횟수  기본 0,0,0
        public List<ObscuredInt> event_package_limit = new List<ObscuredInt>(); // 패키지 구매 제한  기본 0,0,0
    }
    [Serializable]
    public class User_event_pay_info // 유저 이벤트 정보
    {
        public string inDate; // 
        public string event_reset_check;
        public ObscuredInt pay_point; // 교환 포인트 개수  기본 0
        public List<ObscuredInt> pay_reward_check = new List<ObscuredInt>(); // 포인트 교환 횟수  기본 0,0,0,0,0,0      
    }
    [Serializable]
    public class User_event_playTime_info // 유저 이벤트 정보
    {
        public string inDate; // 

        public ObscuredInt playTime; // 교환 포인트 개수  기본 0
        public List<string> playTime_reward_check = new List<string>(); // 포인트 교환 횟수  기본 0,0,0,0,0,0
    }

    [Serializable]
    public class User_event_mission_info // 유저 이벤트 정보
    {
        public string inDate; // 

        public List<ObscuredInt> event_mission_check = new List<ObscuredInt>(); // 미션 보상 수령 여부
        public List<ObscuredInt> event_mission_value = new List<ObscuredInt>(); // 실제 유저의 미션 수치
        public List<ObscuredInt> event_mission_target = new List<ObscuredInt>(); // 미션 달성 조건
        public List<ObscuredInt> event_mission_reward = new List<ObscuredInt>(); // 미션 보상
        public List<ObscuredInt> event_mission_package_check = new List<ObscuredInt>(); // 미션 패키지 카운트
        public List<ObscuredInt> event_mission_package_limit = new List<ObscuredInt>(); // 미션 패키지 수량 제한
    }
    [Serializable]
    public class User_event_1st_anniversary_info // 유저 1주년 이벤트 정보
    {
        public string inDate; // 

        public List<ObscuredInt> event_1st_anniversary_value = new List<ObscuredInt>(); // 
        public List<ObscuredInt> event_1st_anniversary_check = new List<ObscuredInt>(); // 
        public List<ObscuredInt> event_1st_anniversary_bingo_check = new List<ObscuredInt>(); // 
        public List<ObscuredInt> event_1st_anniversary_target = new List<ObscuredInt>(); // 
        public List<ObscuredInt> event_1st_anniversary_reward = new List<ObscuredInt>(); // 
        public List<ObscuredInt> event_1st_anniversary_bingo_reward = new List<ObscuredInt>(); // 

        public List<ObscuredInt> anniversary_attendance_reward = new List<ObscuredInt>(); // 
        public string anniversary_attendance_time; // 
        public ObscuredInt anniversary_attendance_count; // 
        public ObscuredInt anniversary_attendance_check; // 
    }
    [Serializable]
    public class User_year_info // 유저 이벤트 정보
    {
        public string inDate; // 컬렉션 번호

        public ObscuredInt year_attendance_count; //출석 체크 횟수  기본 0 
        public ObscuredInt year_attendance_check; //당일 출석 체크 확인 기본 0
        public string year_attendance_time; // 다음 출석 체크 날자 기본 0
        public List<ObscuredInt> year_attendance_reward = new List<ObscuredInt>(); //출석체크 보상 받은지 기본 0,0,0,0,0,0,0

        public List<ObscuredInt> year_package_check = new List<ObscuredInt>(); // 패키지 구매 횟수  기본 0,0,0,0
    }
    [Serializable]
    public class User_newbie_attendance_info // 유저 이벤트 정보
    {
        public string inDate; // 컬렉션 번호

        public ObscuredInt newbie_attendance_count; //출석 체크 횟수  기본 0 
        public ObscuredInt newbie_attendance_check; //당일 출석 체크 확인 기본 0
        public string newbie_attendance_time; // 다음 출석 체크 날자 기본 0
        public List<ObscuredInt> newbie_attendance_reward = new List<ObscuredInt>(); //출석체크 보상 받은지 기본 0,0,0,0,0,0,0
        public List<ObscuredInt> newbie_attendance_puppy_reward = new List<ObscuredInt>(); // 댕댕이 보상 받았는지 체크
        public List<ObscuredInt> newbie_attendance_pet_reward = new List<ObscuredInt>(); // 신수 보상 받았는지 체크

    }
    [Serializable]
    public class User_attendance_package_info // 유저 이벤트 정보
    {
        public string inDate; // 컬렉션 번호

        public ObscuredInt nomal_attendance_package_check; //출석 체크 횟수  기본 0 
        public ObscuredInt nomal_attendance_package_reward_count; //출석 체크 횟수  기본 0 
        public ObscuredInt nomal_attendance_package_count; //당일 출석 체크 확인 기본 0
        public string nomal_attendance_package_time; // 다음 출석 체크 날자 기본 0
        public List<ObscuredInt> nomal_attendance_package_reward = new List<ObscuredInt>(); //출석체크 보상 받은지 기본 0,0,0,0,0,0,0

        public ObscuredInt premium_attendance_package_check; //출석 체크 횟수  기본 0 
        public ObscuredInt premium_attendance_package_reward_count; //출석 체크 횟수  기본 0 
        public ObscuredInt premium_attendance_package_count; //당일 출석 체크 확인 기본 0
        public string premium_attendance_package_time; // 다음 출석 체크 날자 기본 0
        public List<ObscuredInt> premium_attendance_package_reward = new List<ObscuredInt>(); //출석체크 보상 받은지 기본 0,0,0,0,0,0,0

    }
    [Serializable]
    public class User_rune_info // 유저 이벤트 정보
    {
        public string inDate; // 컬렉션 번호

        public string rune_ad_time; //룬 ad 타임 0
        public ObscuredInt rune_stage;
        public string rune_time;
        public ObscuredInt rune_ticket;
        public List<ObscuredInt> rune_level = new List<ObscuredInt>(); //룬 업그레이드 레벨

        public List<ObscuredInt> rune_stock = new List<ObscuredInt>(); //룬 남은 개수
    }
    [Serializable]
    public class User_pet_info // 유저 이벤트 정보
    {
        public string inDate; // 컬렉션 번호0
        public ObscuredInt select_pet_index; // 선택한 펫
        public ObscuredInt select_pet_type; // 선택한
        public ObscuredInt select_pet_isAwakening; // 선택한 펫
        public ObscuredInt pet_active; // 선택한 펫
        public List<ObscuredInt> pet_awakening_value; //펫 각성 인덱스는 펫 타입과 같다.
        public List<List<ObscuredInt>> user_pet = new List<List<ObscuredInt>>(); //펫 개수
    }
    [Serializable]
    public class Boss_info // 보스정보
    {
        public ObscuredInt boss_grade; // 컬렉션 번호

        public ObscuredLong boss_life;
        public ObscuredLong boss_damage;
        public ObscuredFloat boss_attack_speed;
    }
    [Serializable]
    public class User_boss_info // 보스정보
    {
        public string inDate;

        public List<ObscuredInt> boss_index; // 컬렉션 번호

        public List<ObscuredLong> boss_life;
        public List<ObscuredString> boss_end_time;
        public string ticket_time;
        public ObscuredInt ticket_count;
    }


    [Serializable]
    public class Upgrade_info // 업그레이드 정보
    {
        public int upgrade_index; // 컬렉션 번호

        public string upgrade_name;
        public string upgrade_info;

        public int gold_price;
        public int diamond_price;
        public int again_price;
    }
    [Serializable]
    public class Post_info // 업그레이드 정보
    {
        public string post_indate; // 우편 번호
        public string post_type; // 우편 타입

        public string post_title; // 우편 제목
        public string post_body; // 우편 내용

        public int post_reward_type; // 우편 리워드 타입
        public int post_reward_value; // 우편 리워드 개수
        public int post_reward_rank;

        public string post_send_time; // 우편  보낸 시간
        public string post_expiration;
    }
    [Serializable]
    public class Achievement_info //업적 정보
    {
        public int achievement_index; // 컬렉션 번호

        public string achievement_name;
        public string achievement_info;

        public List<ObscuredInt> list_reward = new List<ObscuredInt>();
        public List<ObscuredInt> list_target = new List<ObscuredInt>();
    }
    [Serializable]
    public class Quest_info //퀘스트 정보
    {
        public int quest_index; // 컬렉션 번호

        public string quest_name;
        public ObscuredInt quest_reward;
        public ObscuredInt quest_target;
    }

    [Serializable]
    public class User_ranking //유닛 정보 
    {
        public int rank;
        public string InDate;
        public string type;
        public string nick_name;
        public long value;
    }

    [Serializable]
    public class pvp_enemy //pvp 적 정보 
    {
        public int unit_index;
        public long power;
        public string nick_name;
        public int attack_damage;
        public float attack_speed;
        public int critical_damage;
        public float critical_percent;
        public int life;
    }
    [Serializable]
    public class pvp_user //pvp 적 정보 
    {
        public int unit_index;
        public string indate;
        public string nick_name;
        public int attack_damage;
        public float attack_speed;
        public int critical_damage;
        public float critical_percent;
        public int life;
    }
    public class Attendance_info //퀘스트 정보
    {
        public List<int> attendance_reward_value = new List<int>();
        public List<int> attendance_reward_type = new List<int>();
        public List<int> attendance_special_reward_value = new List<int>();
        public List<int> attendance_special_reward_type = new List<int>();
    }
    [Serializable]
    public class User_info    //유저 정보
    {
        public string inDate;
        public string nickname;
        public string id;
        public ObscuredInt pvp_point;
        public ObscuredLong gold;
        public ObscuredLong diamond;
        public ObscuredLong point;

        public ObscuredLong power;

        public List<ObscuredInt> user_unit = new List<ObscuredInt>();
        public ObscuredInt select_unit;
        public ObscuredLong user_stage;

        public string tresure_box_reward_time = "0";
        public ObscuredInt review;
        public string notice_time = "0";
        public string save_gold_time = "0";
        //조련사
        public ObscuredInt user_level;
        public ObscuredInt user_skill_point;
        public ObscuredInt user_experience;
        public List<ObscuredInt> user_skill = new List<ObscuredInt>();

        //업적
        public List<ObscuredInt> user_achievements = new List<ObscuredInt>();
        public ObscuredLong monster_count;
        public ObscuredInt boss_count;
        public ObscuredInt ad_count;
        public ObscuredInt total_stage;
        public ObscuredLong best_stage;
        public ObscuredInt tresure_box_count;
        public ObscuredInt total_again;

        //일일 퀘스트
        public List<ObscuredInt> user_quest_value = new List<ObscuredInt>();
        public List<ObscuredInt> user_quest_reward = new List<ObscuredInt>();

        //출석체크
        public ObscuredInt attendance_check;
        public string attendance_time;
        public ObscuredInt attendance_count;
        public List<ObscuredInt> attendance_reward = new List<ObscuredInt>();
        public List<ObscuredInt> attendance_special_reward = new List<ObscuredInt>();
        public ObscuredInt attendance_level;

        //유저 업그레이드

        //골드
        public ObscuredInt upgrade_gold_attack_damage;
        public ObscuredInt upgrade_gold_attack_speed;
        public ObscuredInt upgrade_gold_critical_damage;
        public ObscuredInt upgrade_gold_critical_percent;
        public ObscuredInt upgrade_gold_life;
        public ObscuredInt upgrade_gold_enemy_life;
        public ObscuredInt upgrade_gold_again;

        //다이아
        public ObscuredInt upgrade_diamond_attack_damage;
        public ObscuredInt upgrade_diamond_attack_speed;
        public ObscuredInt upgrade_diamond_critical_damage;
        public ObscuredInt upgrade_diamond_critical_percent;
        public ObscuredInt upgrade_diamond_life;
        public ObscuredInt upgrade_diamond_enemy_life;
        public ObscuredInt upgrade_diamond_again;

        //환생
        public ObscuredInt upgrade_point_attack_damage;
        public ObscuredInt upgrade_point_attack_speed;
        public ObscuredInt upgrade_point_critical_damage;
        public ObscuredInt upgrade_point_critical_percent;
        public ObscuredInt upgrade_point_life;
        public ObscuredInt upgrade_point_enemy_life;
        public ObscuredInt upgrade_point_again;

        //아이템
        public List<ObscuredInt> user_item = new List<ObscuredInt>();
        public List<string> user_item_time = new List<string>();
        public List<string> user_item_ad_time = new List<string>();
        public int user_item_power_ad_count = 0; // 광고로 간식 아이템 수령 횟수 20회 제한
        public int user_item_speed_ad_count = 0; // 광고로 간식 아이템 수령 횟수 20회 제한

        public int user_rune_ad_count = 0; // 광고로 룬 뽑기 횟수 20회 제한

        public string last_connected_time = DateTime.Now.ToString();

        public string pvp_time;
        public string pvp_end_time;
        public ObscuredInt pvp_ticket;
        public string pvp_ad_time;

        public List<ObscuredInt> compose_stack = new List<ObscuredInt>();

        public ObscuredInt free_diamond_ticket;
        public string free_diamond_ad_time;
        public ObscuredInt point_treasure_ticket;
        public ObscuredInt package_active;
        public string package_active_time;
        public int start_package;
        public int gold_start_package;
        //미트 개편 이후 새로운 스타트 패키지
        public int new_start_package;
        public int new_gold_start_package;

        public int auto_again_active;
        public int auto_again_setting;
        public int auto_again_stage;
        public string auto_again_time;

        //스킨
        public int user_skin_state; //스킨 온오프상태
        public List<int> user_skin_list; //스킨 리스트
        public int user_current_skin; //현재 적용중인 스킨

        public int item_package_purchase; //간식패키지구매여부
        public string item_package_time; //간식패키지기간

    }

    public int unit_count = 0;
    public int unit_collection_count = 0;
    public int pet_collection_count = 0;

    public int gold_attack_damage_max = 0;
    public int gold_attack_speed_max = 0;
    public int gold_attack_critical_damage_max = 0;
    public int gold_attack_critical_percent_max = 0;
    public int gold_life_max = 0;
    public int gold_again_max = 0;

    public int point_attack_damage_max = 0;
    public int point_attack_speed_max = 0;
    public int point_attack_critical_damage_max = 0;
    public int point_attack_critical_percent_max = 0;
    public int point_life_max = 0;
    public int point_again_max = 0;

    public bool[] buy_event_package = new bool[5];

    void Awake()
    {
        unit_count = 40;
        unit_collection_count = 23;
        pet_collection_count = 24;


        // 골드 업그레이드 최대치
        gold_attack_damage_max = 9999;
        gold_attack_speed_max = 199;
        gold_attack_critical_damage_max = 2999;
        gold_attack_critical_percent_max = 99;
        gold_life_max = 9999;
        gold_again_max = 99;

        // 포인트 업그레이드 최대치
        point_attack_damage_max = 4999;
        point_attack_speed_max = 199;
        point_attack_critical_damage_max = 2999;
        point_attack_critical_percent_max = 99;
        point_life_max = 4999;
        point_again_max = 99;

        for (int i = 0; i < buy_event_package.Length; i++)
        {
            buy_event_package[i] = false;
        }

        Application.runInBackground = true;
        Application.targetFrameRate = 60;
        DynamicLinks.DynamicLinkReceived += OnDynamicLink;
        if (Application.isEditor)
        {
            editor_check = true;
        }
        else
        {
            editor_check = false;
        }
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
        Debug.Log("DB START");
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //Screen.SetResolution(Screen.height, Screen.height * 16 / 9, true);
        terms_date_load(); // 이용약관 불러옴

        if (PlayerPrefs.HasKey("Bgm")) // bgm 정보가 존재 한다면
        {
            bgm_value = PlayerPrefs.GetFloat("Bgm");
            SoundManager.instance.bgm_sound.volume = bgm_value;
        }
        else
        {
            bgm_value = 1;
            PlayerPrefs.SetFloat("Bgm", 1f);
        }
        if (PlayerPrefs.HasKey("Sfx")) // sfx 정보가 존재 한다면
        {
            sfx_value = PlayerPrefs.GetFloat("Sfx");
            SoundManager.instance.sfx_sound.volume = sfx_value;
        }
        else
        {
            sfx_value = 1;
            PlayerPrefs.SetFloat("Sfx", 1f);
        }

        if (Application.systemLanguage.ToString() == "Korean")
        {
            language_index = 0;
            if (!PlayerPrefs.HasKey("language"))
            {
                PlayerPrefs.SetInt("language", 0);
            }
        }
        else if (Application.systemLanguage.ToString() == "English")
        {
            language_index = 1;
            if (!PlayerPrefs.HasKey("language"))
            {
                PlayerPrefs.SetInt("language", 1);
            }
        }
        else if (Application.systemLanguage.ToString() == "Japanese")
        {
            language_index = 1;
            if (!PlayerPrefs.HasKey("language"))
            {
                PlayerPrefs.SetInt("language", 1);
            }
        }
        else if (Application.systemLanguage.ToString() == "ChineseSimplified")
        {
            language_index = 1;
            if (!PlayerPrefs.HasKey("language"))
            {
                PlayerPrefs.SetInt("language", 1);
            }
        }
        else if (Application.systemLanguage.ToString() == "ChineseTraditional")
        {
            language_index = 1;
            if (!PlayerPrefs.HasKey("language"))
            {
                PlayerPrefs.SetInt("language", 1);
            }
        }
        else
        {
            language_index = 1;
            if (!PlayerPrefs.HasKey("language"))
            {
                PlayerPrefs.SetInt("language", 1);
            }
        }
        Language_parsing();
    }
    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void Update()
    {
        if (user_info_update_time >= 0)
            user_info_update_time += Time.deltaTime;
        if (user_mission_update_time >= 0)
            user_mission_update_time += Time.deltaTime;
        if (user_1st_anniversary_update_time >= 0)
            user_1st_anniversary_update_time += Time.deltaTime;
        if (check_enemy_update_time > 0)
        {
            check_enemy_update_time -= Time.deltaTime;
            if (check_enemy_update_time < 0)
            {
                check_enemy_update_time = 0;
            }
        }
    }

    void OnDynamicLink(object sender, EventArgs args)
    {
        var dynamicLinkEventArgs = args as ReceivedDynamicLinkEventArgs;
        Debug.LogFormat("Received dynamic link {0}",
                        dynamicLinkEventArgs.ReceivedDynamicLink.Url.OriginalString);
    }

    //유저 정보 진행상황이나 보유 댕댕이,펫 출석 등 서버로부터 데이터 받아오기
    public void DB_Insert_user_info(JsonData json) // 유저 데이터 DB 인서트
    {
        Debug.Log("-DB_Insert_user_info-");
        // Debug.Log("insert user info " + json.ToJson());
        char[] splitter = { ',' };

        Debug.Log("유저 인포 버그 테스트 1");
        user_info.inDate = json[1][0]["inDate"]["S"].ToString();
        user_info.pvp_point = int.Parse(json[1][0]["pvp_point"]["N"].ToString());
        user_info.gold = Int64.Parse(json[1][0]["gold"]["N"].ToString());
        user_info.diamond = int.Parse(json[1][0]["diamond"]["N"].ToString());
        user_info.point = Int64.Parse(json[1][0]["point"]["N"].ToString());
        user_info.power = Int64.Parse(json[1][0]["power"]["N"].ToString());
        // Debug.Log("유저 인포 버그 테스트 2");
        string[] splitter_user_unit = json[1][0]["user_unit"]["S"].ToString().Split(splitter);
        user_info.user_unit.Clear();
        for (int s = 0; s < splitter_user_unit.Length; s++)
        {
            user_info.user_unit.Add(int.Parse(splitter_user_unit[s].ToString()));
        }
        while (true)
        {
            if (user_info.user_unit.Count < list_unit_info.Count)
            {
                user_info.user_unit.Add(0);
                if (user_info.user_unit.Count >= list_unit_info.Count)
                    break;
            }
            else
                break;
        }
        user_info.select_unit = int.Parse(json[1][0]["select_unit"]["N"].ToString());
        user_info.user_stage = int.Parse(json[1][0]["user_stage"]["N"].ToString());
        user_info.tresure_box_reward_time = json[1][0]["tresure_box_reward_time"]["S"].ToString();
        user_info.review = int.Parse(json[1][0]["review"]["S"].ToString());
        user_info.notice_time = json[1][0]["notice_time"]["S"].ToString();
        user_info.save_gold_time = json[1][0]["save_gold_time"]["S"].ToString();
        user_info.user_level = int.Parse(json[1][0]["user_level"]["N"].ToString());
        user_info.user_skill_point = int.Parse(json[1][0]["user_skill_point"]["N"].ToString());
        user_info.user_experience = int.Parse(json[1][0]["user_experience"]["N"].ToString());
        string[] splitter_user_skill = json[1][0]["user_skill"]["S"].ToString().Split(splitter);
        user_info.user_skill.Clear();
        for (int s = 0; s < 6; s++)
        {
            user_info.user_skill.Add(int.Parse(splitter_user_skill[s].ToString()));
        }
        string[] splitter_achievements = json[1][0]["achievements"]["S"].ToString().Split(splitter);
        user_info.user_achievements.Clear();
        for (int s = 0; s < splitter_achievements.Length; s++)
        {
            user_info.user_achievements.Add(int.Parse(splitter_achievements[s]));
        }
        while (user_info.user_achievements.Count < list_achievement_info.Count)
        {
            user_info.user_achievements.Add(0);
        }
        user_info.monster_count = int.Parse(json[1][0]["monster_count"]["N"].ToString());
        user_info.boss_count = int.Parse(json[1][0]["boss_count"]["N"].ToString());
        user_info.ad_count = int.Parse(json[1][0]["ad_count"]["N"].ToString());
        user_info.best_stage = long.Parse(json[1][0]["best_stage"]["N"].ToString());
        user_info.total_stage = int.Parse(json[1][0]["total_stage"]["N"].ToString());
        user_info.tresure_box_count = int.Parse(json[1][0]["tresure_box_count"]["N"].ToString());
        user_info.total_again = int.Parse(json[1][0]["total_again"]["N"].ToString());
        string[] splitter_user_quest_reward = json[1][0]["user_quest_reward"]["S"].ToString().Split(splitter);
        string[] splitter_user_quest_value = json[1][0]["user_quest_value"]["S"].ToString().Split(splitter);
        user_info.user_quest_reward.Clear();
        user_info.user_quest_value.Clear();
        for (int s = 0; s < splitter_user_quest_reward.Length; s++)
        {
            user_info.user_quest_reward.Add(int.Parse(splitter_user_quest_reward[s].ToString()));
            user_info.user_quest_value.Add(int.Parse(splitter_user_quest_value[s].ToString()));
        }
        while (user_info.user_quest_reward.Count < list_quest_info.Count)
        {
            user_info.user_quest_reward.Add(0);
        }
        while (user_info.user_quest_value.Count < list_quest_info.Count)
        {
            user_info.user_quest_value.Add(0);
        }
        user_info.user_item.Clear();
        string[] splitter_user_item = json[1][0]["user_item"]["S"].ToString().Split(splitter);
        for (int s = 0; s < 5; s++)
        {
            user_info.user_item.Add(int.Parse(splitter_user_item[s].ToString()));
        }
        string[] splitter_user_item_time = json[1][0]["user_item_time"]["S"].ToString().Split(splitter);
        user_info.user_item_time.Clear();
        for (int s = 0; s < 4; s++)
        {
            user_info.user_item_time.Add(splitter_user_item_time[s].ToString());
        }
        string[] splitter_user_item_ad_time = json[1][0]["user_item_ad_time"]["S"].ToString().Split(splitter);
        user_info.user_item_ad_time.Clear();
        for (int s = 0; s < 4; s++)
        {
            user_info.user_item_ad_time.Add(splitter_user_item_ad_time[s].ToString());
        }
        string[] splitter_attendance_reward = json[1][0]["attendance_reward"]["S"].ToString().Split(splitter);
        user_info.attendance_check = int.Parse(json[1][0]["attendance_check"]["N"].ToString());
        user_info.attendance_time = json[1][0]["attendance_time"]["S"].ToString();
        user_info.attendance_count = int.Parse(json[1][0]["attendance_count"]["N"].ToString());
        user_info.attendance_reward.Clear();
        for (int s = 0; s < 30; s++)
        {
            user_info.attendance_reward.Add(int.Parse(splitter_attendance_reward[s].ToString()));
        }
        user_info.attendance_level = int.Parse(json[1][0]["attendance_level"]["N"].ToString());
        user_info.attendance_special_reward.Clear();
        string[] splitter_attendance_special_reward = json[1][0]["attendance_special_reward"]["S"].ToString().Split(splitter);
        for (int s = 0; s < 3; s++)
        {
            user_info.attendance_special_reward.Add(int.Parse(splitter_attendance_special_reward[s].ToString()));
        }
        string[] splitter_upgrade_gold = json[1][0]["upgrade_gold"]["S"].ToString().Split(splitter);
        string[] splitter_upgrade_diamond = json[1][0]["upgrade_diamond"]["S"].ToString().Split(splitter);
        string[] splitter_upgrade_point = json[1][0]["upgrade_point"]["S"].ToString().Split(splitter);

        user_info.upgrade_gold_attack_damage = int.Parse(splitter_upgrade_gold[0].ToString());
        user_info.upgrade_gold_attack_speed = int.Parse(splitter_upgrade_gold[1].ToString());
        user_info.upgrade_gold_critical_damage = int.Parse(splitter_upgrade_gold[2].ToString());
        user_info.upgrade_gold_critical_percent = int.Parse(splitter_upgrade_gold[3].ToString());
        user_info.upgrade_gold_life = int.Parse(splitter_upgrade_gold[4].ToString());
        user_info.upgrade_gold_enemy_life = int.Parse(splitter_upgrade_gold[5].ToString());
        user_info.upgrade_gold_again = int.Parse(splitter_upgrade_gold[6].ToString());
        user_info.upgrade_diamond_attack_damage = int.Parse(splitter_upgrade_diamond[0].ToString());
        user_info.upgrade_diamond_attack_speed = int.Parse(splitter_upgrade_diamond[1].ToString());
        user_info.upgrade_diamond_critical_damage = int.Parse(splitter_upgrade_diamond[2].ToString());
        user_info.upgrade_diamond_critical_percent = int.Parse(splitter_upgrade_diamond[3].ToString());
        user_info.upgrade_diamond_life = int.Parse(splitter_upgrade_diamond[4].ToString());
        user_info.upgrade_diamond_enemy_life = int.Parse(splitter_upgrade_diamond[5].ToString());
        user_info.upgrade_diamond_again = int.Parse(splitter_upgrade_diamond[6].ToString());
        user_info.upgrade_point_attack_damage = int.Parse(splitter_upgrade_point[0].ToString());
        user_info.upgrade_point_attack_speed = int.Parse(splitter_upgrade_point[1].ToString());
        user_info.upgrade_point_critical_damage = int.Parse(splitter_upgrade_point[2].ToString());
        user_info.upgrade_point_critical_percent = int.Parse(splitter_upgrade_point[3].ToString());
        user_info.upgrade_point_life = int.Parse(splitter_upgrade_point[4].ToString());
        user_info.upgrade_point_enemy_life = int.Parse(splitter_upgrade_point[5].ToString());
        user_info.upgrade_point_again = int.Parse(splitter_upgrade_point[6].ToString());

        user_info.pvp_time = json[1][0]["pvp_time"]["S"].ToString();
        IDictionary check_userdata_is_null = json[1][0] as IDictionary;

        if (check_userdata_is_null.Contains("free_diamond_ticket"))
        {
            user_info.free_diamond_ticket = int.Parse(json[1][0]["free_diamond_ticket"]["S"].ToString());
        }
        else
        {
            user_info.free_diamond_ticket = 10;
        }

        if (check_userdata_is_null.Contains("free_diamond_ad_time"))
        {
            user_info.free_diamond_ad_time = json[1][0]["free_diamond_ad_time"]["S"].ToString();
        }
        else
        {
            user_info.free_diamond_ad_time = "0";
        }

        if (check_userdata_is_null.Contains("point_treasure_ticket"))
        {
            user_info.point_treasure_ticket = int.Parse(json[1][0]["point_treasure_ticket"]["S"].ToString());
        }
        else
        {
            user_info.point_treasure_ticket = 10;
        }

        if (check_userdata_is_null.Contains("package_active"))
        {
            user_info.package_active = int.Parse(json[1][0]["package_active"]["S"].ToString());
        }
        else
        {
            user_info.package_active = 0;
        }

        if (check_userdata_is_null.Contains("package_active_time"))
        {
            user_info.package_active_time = json[1][0]["package_active_time"]["S"].ToString();
        }
        else
        {
            user_info.package_active_time = "0";
        }

        if (check_userdata_is_null.Contains("start_package"))
        {
            user_info.start_package = int.Parse(json[1][0]["start_package"]["S"].ToString());
        }
        else
        {
            user_info.start_package = 0;
        }

        if (check_userdata_is_null.Contains("gold_start_package"))
        {
            user_info.gold_start_package = int.Parse(json[1][0]["gold_start_package"]["S"].ToString());
        }
        else
        {
            user_info.gold_start_package = 0;
        }

        if (check_userdata_is_null.Contains("pvp_end_time"))
        {
            user_info.pvp_end_time = json[1][0]["pvp_end_time"]["S"].ToString();
        }
        else
        {
            user_info.pvp_end_time = "0";
        }

        if (check_userdata_is_null.Contains("pvp_ticket"))
        {
            user_info.pvp_ticket = int.Parse(json[1][0]["pvp_ticket"]["S"].ToString());
        }
        else
        {
            user_info.pvp_ticket = 4;
        }

        if (check_userdata_is_null.Contains("pvp_ad_time"))
        {
            user_info.pvp_ad_time = json[1][0]["pvp_ad_time"]["S"].ToString();
        }
        else
        {
            user_info.pvp_ad_time = "0";
        }

        if (check_userdata_is_null.Contains("user_compose_count"))
        {
            user_info.compose_stack.Clear();
            string[] splitter_compose_stack = json[1][0]["user_compose_count"]["S"].ToString().Split(splitter);
            for (int s = 0; s < 7; s++)
            {
                user_info.compose_stack.Add(int.Parse(splitter_compose_stack[s].ToString()));
            }
        }
        else
        {
            user_info.compose_stack.Clear();
            for (int i = 0; i < 7; i++)
            {
                user_info.compose_stack.Add(0);
            }
        }

        if (check_userdata_is_null.Contains("auto_again_active"))
        {
            user_info.auto_again_active = int.Parse(json[1][0]["auto_again_active"]["S"].ToString());
        }
        else
        {
            user_info.auto_again_active = 0;
        }

        if (check_userdata_is_null.Contains("auto_again_setting"))
        {
            user_info.auto_again_setting = int.Parse(json[1][0]["auto_again_setting"]["S"].ToString());
        }
        else
        {
            user_info.auto_again_setting = 0;
        }

        if (check_userdata_is_null.Contains("auto_again_stage"))
        {
            user_info.auto_again_stage = int.Parse(json[1][0]["auto_again_stage"]["S"].ToString());
        }
        else
        {
            user_info.auto_again_stage = 0;
        }

        if (check_userdata_is_null.Contains("auto_again_time"))
        {
            user_info.auto_again_time = json[1][0]["auto_again_time"]["S"].ToString();
        }
        else
        {
            user_info.auto_again_time = "0";
        }

        //user skin
        if (check_userdata_is_null.Contains("user_skin_state"))
        {
            user_info.user_skin_state = int.Parse(json[1][0]["user_skin_state"]["S"].ToString());
        }
        else
        {
            user_info.user_skin_state = 0;
        }
        if (check_userdata_is_null.Contains("user_skin_list"))
        {
            string[] user_skin_list_split = json[1][0]["user_skin_list"]["S"].ToString().Split(splitter);
            for (int i = 0; i < 10; i++)
            {
                user_info.user_skin_list.Add(int.Parse(user_skin_list_split[i].ToString()));
            }
        }
        else
        {
            user_info.user_skin_list.Clear();
            for (int i = 0; i < 10; i++)
            {
                user_info.user_skin_list.Add(0);
            }
        }
        if (check_userdata_is_null.Contains("user_current_skin"))
        {
            user_info.user_current_skin = int.Parse(json[1][0]["user_current_skin"]["S"].ToString());
        }
        else
        {
            user_info.user_current_skin = 0;
        }

        //간식 패키지 구매 여부
        if (check_userdata_is_null.Contains("item_package_purchase"))
        {
            user_info.item_package_purchase = int.Parse(json[1][0]["item_package_purchase"]["S"].ToString());
        }
        else
        {
            user_info.item_package_purchase = 0;
        }

        //간식 패키지 시간
        if (check_userdata_is_null.Contains("item_package_time"))
        {
            user_info.item_package_time = json[1][0]["item_package_time"]["S"].ToString();
        }
        else
        {
            user_info.item_package_time = "0";
        }

        //공격력 간식 광고 횟수 제한
        if (check_userdata_is_null.Contains("user_item_power_ad_count"))
        {
            user_info.user_item_power_ad_count = int.Parse(json[1][0]["user_item_power_ad_count"]["S"].ToString());
        }
        else
        {
            user_info.user_item_power_ad_count = 20;
        }
        //공격속도 간식 광고 횟수 제한
        if (check_userdata_is_null.Contains("user_item_speed_ad_count"))
        {
            user_info.user_item_speed_ad_count = int.Parse(json[1][0]["user_item_speed_ad_count"]["S"].ToString());
        }
        else
        {
            user_info.user_item_speed_ad_count = 20;
        }
        //룬 광고 횟수 제한
        if (check_userdata_is_null.Contains("user_rune_ad_count"))
        {
            user_info.user_rune_ad_count = int.Parse(json[1][0]["user_rune_ad_count"]["S"].ToString());
        }
        else
        {
            user_info.user_rune_ad_count = 20;
        }

        /*최근 접속 시간
            기존 간식 시간 시스템 : 구매 시 -> 현재 시간 +30분 해서 그 시간 까지 지속되도록

            기존 시스템에서는 미접속시에는 시간 안 흐르게 할 수 없음->그만큼 보정을 줘야함

            보정 값 계산 방식 : 현재 시간 += 미접속한 시간 만큼

            미접속한 시간 구하기->user_info에 저장
        */
        if (check_userdata_is_null.Contains("last_connected_time"))
        {
            user_info.last_connected_time = json[1][0]["last_connected_time"]["S"].ToString();
            Debug.LogWarning("보정 이전 공격력 간식 시간" + user_info.user_item_time[0]);
            Debug.LogWarning("보정 이전 공격속도 간식 시간" + user_info.user_item_time[1]);
            Debug.LogWarning("보정 이전 환생 아이템 시간" + user_info.user_item_time[2]);

            Debug.LogWarning("servertime : " + ServerManager.Instance.server_time);


            TimeSpan timeCorrectionValue = ServerManager.Instance.server_time - DateTime.Parse(user_info.last_connected_time); //아이템 시간 보정 값
            Debug.LogWarning("최근 접속 시간" + user_info.last_connected_time);
            Debug.LogWarning("시간 보정 값" + timeCorrectionValue);

            if (user_info.user_item_time[0] != "0")
            {
                DateTime corrected_power_time = DateTime.Parse(user_info.user_item_time[0]).Add(timeCorrectionValue);
                user_info.user_item_time[0] = corrected_power_time.ToString("MM/dd/yyyy H:mm:ss");
                Debug.LogWarning("보정된 공격력 간식 시간" + user_info.user_item_time[0]);
            }

            if (user_info.user_item_time[1] != "0")
            {
                DateTime corrected_speed_time = DateTime.Parse(user_info.user_item_time[1]).Add(timeCorrectionValue);
                user_info.user_item_time[1] = corrected_speed_time.ToString("MM/dd/yyyy H:mm:ss");
                Debug.LogWarning("보정된 공격속도 간식 시간" + user_info.user_item_time[1]);
            }

            if (user_info.user_item_time[2] != "0")
            {
                DateTime corrected_speed_time = DateTime.Parse(user_info.user_item_time[2]).Add(timeCorrectionValue);
                user_info.user_item_time[2] = corrected_speed_time.ToString("MM/dd/yyyy H:mm:ss");
                Debug.LogWarning("보정된 환생아이템 시간" + user_info.user_item_time[2]);
            }

        }
        else
        {
            user_info.last_connected_time = DateTime.Now.ToString("MM/dd/yyyy H:mm:ss");
            Debug.LogWarning("보정 이전 공격력 간식 시간" + user_info.user_item_time[0]);
            Debug.LogWarning("보정 이전 공격속도 간식 시간" + user_info.user_item_time[1]);

            Debug.LogWarning("servertime : " + ServerManager.Instance.server_time);
            TimeSpan timeCorrectionValue = ServerManager.Instance.server_time - DateTime.Parse(user_info.last_connected_time); //아이템 시간 보정 값
            Debug.LogWarning("최근 접속 시간" + user_info.last_connected_time);
            Debug.LogWarning("시간 보정 값" + timeCorrectionValue);

            if (user_info.user_item_time[0] != "0")
            {
                DateTime corrected_power_time = DateTime.Parse(user_info.user_item_time[0]).Add(timeCorrectionValue);
                user_info.user_item_time[0] = corrected_power_time.ToString("MM/dd/yyyy H:mm:ss");
                Debug.LogWarning("보정된 공격력 간식 시간" + user_info.user_item_time[0]);
            }

            if (user_info.user_item_time[1] != "0")
            {
                DateTime corrected_speed_time = DateTime.Parse(user_info.user_item_time[1]).Add(timeCorrectionValue);
                user_info.user_item_time[1] = corrected_speed_time.ToString("MM/dd/yyyy H:mm:ss");
                Debug.LogWarning("보정된 공격속도 간식 시간" + user_info.user_item_time[1]);
            }

            if (user_info.user_item_time[2] != "0")
            {
                DateTime corrected_speed_time = DateTime.Parse(user_info.user_item_time[2]).Add(timeCorrectionValue);
                user_info.user_item_time[2] = corrected_speed_time.ToString("MM/dd/yyyy H:mm:ss");
                Debug.LogWarning("보정된 환생아이템 시간" + user_info.user_item_time[2]);
            }
        }

        //미트 개편 이후 새 스타트 패키지
        if (check_userdata_is_null.Contains("new_start_package"))
        {
            user_info.new_start_package = int.Parse(json[1][0]["new_start_package"]["S"].ToString());
        }
        else
        {
            user_info.new_start_package = 0;
        }

        //미트 개편 이후 새 골드 스타트 패키지
        if (check_userdata_is_null.Contains("new_gold_start_package"))
        {
            user_info.new_gold_start_package = int.Parse(json[1][0]["new_gold_start_package"]["S"].ToString());
        }
        else
        {
            user_info.new_gold_start_package = 0;
        }

        ServerManager.Instance.get_user_info_table_check = true;
        //
    }

    //유저 진행 정보 서버로 데이터 업데이트
    //몬스터.cs 레벨업 , GameManager nextstage 에서 호출
    public void DB_Update_user_info_stage()
    {
        if (user_info_update_time > 180)
        {
            user_info_update_time = 0;
            Debug.Log("-DB_Update_user_info_stage-");
            Param param = new Param();
            param.Add("gold", user_info.gold.GetDecrypted());   //골드
            param.Add("user_stage", user_info.user_stage.GetDecrypted());   //현재 스테이지
            param.Add("user_level", user_info.user_level.GetDecrypted());   //레벨
            param.Add("user_skill_point", user_info.user_skill_point.GetDecrypted());   //스킬포인트
            param.Add("user_experience", user_info.user_experience.GetDecrypted());     //경험치
            param.Add("monster_count", user_info.monster_count.GetDecrypted());     //몬스터 잡은 마릿 수
            param.Add("best_stage", user_info.best_stage.GetDecrypted());       //최고 스테이지
            param.Add("total_stage", user_info.total_stage.GetDecrypted());     //총 스테이지
            param.Add("last_connected_time", user_info.last_connected_time);

            //Debug.Log(param.GetJson());
            Backend.GameData.Update("user_info", user_info.inDate, param, (callback) =>
            {
                if (callback.IsSuccess())
                {
                    retry = 0;

                    if (SceneManager.GetActiveScene().name == "GameScene")
                    {
                        UIManager.Instance.top_panel.User_gold_setting();
                        UIManager.Instance.game_panel.User_item_setting();
                    }

                    //Debug.Log("------------------------user_info_update success");
                }
                else
                {
                    retry++;
                    if (retry <= 4) //횟수 4회 미만시 재시도
                    {
                        Debug.LogWarningFormat("DB Stage info Update {0}회 재시도", retry);
                        user_info_update_time = 180;
                        DB_Update_user_info_stage();
                    }
                    else if (retry > 4) //횟수 4회 이상 초과시 그만
                    {
                        Debug.LogWarningFormat("DB Stage info Update {0}회 실패로 재시도 중단", retry);
                        ServerManager.Instance.error_type = 19;
                        ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString(), param.GetJson());
                    }
                    else
                    {
                        //재시도 시 15초 마다 재실행
                        user_info_update_time = 180;
                        Debug.LogWarningFormat("DB Stage info Update {0}회 째 15초 뒤에 실행", retry);
                        Invoke("DB_Update_user_info_stage", 15f);
                    }
                }
            });
        }
        else
        {
            return;
        }
    }

    //유저 정보 서버로 업데이트
    public void DB_Update_userinfo_treasure()
    {
        Debug.Log("-DB_Update_userinfo_treasure-");
        // Debug.Log("=============================유저 정보 업데이트=================================");
        Param param = new Param();

        param.Add("gold", user_info.gold.GetDecrypted());
        param.Add("diamond", user_info.diamond.GetDecrypted());
        param.Add("point", user_info.point.GetDecrypted());

        param.Add("user_stage", user_info.user_stage.GetDecrypted());

        param.Add("user_quest_value", Update_list_converter(user_info.user_quest_value));
        param.Add("user_quest_reward", Update_list_converter(user_info.user_quest_reward));

        param.Add("user_unit", Update_list_converter(user_info.user_unit));
        param.Add("tresure_box_count", user_info.tresure_box_count.GetDecrypted());
        param.Add("tresure_box_reward_time", user_info.tresure_box_reward_time);
        param.Add("point_treasure_ticket", user_info.point_treasure_ticket.GetDecrypted().ToString());


        Backend.GameData.Update("user_info", user_info.inDate, param, (callback) =>
        {

            if (callback.GetStatusCode() == "204")
            {
                /// Debug.Log("------------------------user_info_update success");

                if (SceneManager.GetActiveScene().name == "GameScene")
                {
                    UIManager.Instance.top_panel.User_gold_setting();
                    UIManager.Instance.game_panel.User_item_setting();
                }
            }
            else
            {
                ServerManager.Instance.error_type = 30;
                ServerManager.Instance.server_loading = false;
                ServerManager.Instance.Server_connect_check();
                ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString(), param.GetJson());
            }
        });
    }

    //유저 퀘스트 진행 정보 업데이트
    public void DB_Update_userinfo_quest()
    {
        Debug.Log("-DB_Update_userinfo_quest-");
        // Debug.Log("=============================유저 정보 업데이트=================================");
        Param param = new Param();

        param.Add("gold", user_info.gold.GetDecrypted());
        param.Add("diamond", user_info.diamond.GetDecrypted());
        param.Add("point", user_info.point.GetDecrypted());

        param.Add("user_stage", user_info.user_stage.GetDecrypted());

        param.Add("user_quest_value", Update_list_converter(user_info.user_quest_value));
        param.Add("user_quest_reward", Update_list_converter(user_info.user_quest_reward));


        Backend.GameData.Update("user_info", user_info.inDate, param, (callback) =>
        {

            if (callback.GetStatusCode() == "204")
            {
                /// Debug.Log("------------------------user_info_update success");

                if (SceneManager.GetActiveScene().name == "GameScene")
                {
                    UIManager.Instance.top_panel.User_gold_setting();
                    UIManager.Instance.game_panel.User_item_setting();
                }
            }
            else
            {
                ServerManager.Instance.error_type = 31;
                ServerManager.Instance.server_loading = false;
                ServerManager.Instance.Server_connect_check();
                ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString(), param.GetJson());
            }

        });
    }

    //유저 진행 정보, 강화 시스템 서버로 업데이트
    public void DB_Update_userinfo_upgrade()
    {
        Debug.Log("-DB_Update_userinfo_upgrade-");
        Param param = new Param();

        param.Add("gold", user_info.gold.GetDecrypted());
        param.Add("diamond", user_info.diamond.GetDecrypted());
        param.Add("point", user_info.point.GetDecrypted());

        param.Add("user_stage", user_info.user_stage.GetDecrypted());
        // Debug.Log("유저 인포 버그 테스트 13");
        param.Add("upgrade_gold", user_info.upgrade_gold_attack_damage.ToString() + "," + user_info.upgrade_gold_attack_speed.ToString() + "," + user_info.upgrade_gold_critical_damage.ToString() + "," + user_info.upgrade_gold_critical_percent.ToString() + "," + user_info.upgrade_gold_life.ToString() + "," + user_info.upgrade_gold_enemy_life.ToString() + "," + user_info.upgrade_gold_again.ToString());
        param.Add("upgrade_diamond", user_info.upgrade_diamond_attack_damage.ToString() + "," + user_info.upgrade_diamond_attack_speed.ToString() + "," + user_info.upgrade_diamond_critical_damage.ToString() + "," + user_info.upgrade_diamond_critical_percent.ToString() + "," + user_info.upgrade_diamond_life.ToString() + "," + user_info.upgrade_diamond_enemy_life.ToString() + "," + user_info.upgrade_diamond_again.ToString());
        param.Add("upgrade_point", user_info.upgrade_point_attack_damage.ToString() + "," + user_info.upgrade_point_attack_speed.ToString() + "," + user_info.upgrade_point_critical_damage.ToString() + "," + user_info.upgrade_point_critical_percent.ToString() + "," + user_info.upgrade_point_life.ToString() + "," + user_info.upgrade_point_enemy_life.ToString() + "," + user_info.upgrade_point_again.ToString());


        Backend.GameData.Update("user_info", user_info.inDate, param, (callback) =>
        {

            if (callback.GetStatusCode() == "204")
            {
                /// Debug.Log("------------------------user_info_update success");

                if (SceneManager.GetActiveScene().name == "GameScene")
                {
                    UIManager.Instance.top_panel.User_gold_setting();
                    UIManager.Instance.game_panel.User_item_setting();
                }
            }
            else
            {
                ServerManager.Instance.error_type = 32;
                ServerManager.Instance.server_loading = false;
                ServerManager.Instance.Server_connect_check();
                ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString(), param.GetJson());
            }

        });
    }

    //모든 유저 진행 정보 서버로 업데이트
    public void DB_Update_user_info()
    {
        Debug.Log("-DB_Update_user_info-");
        //Debug.Log("=============================유저 정보 업데이트=================================");

        Param param = new Param();
        param.Add("pvp_point", user_info.pvp_point.GetDecrypted());
        param.Add("gold", user_info.gold.GetDecrypted());
        param.Add("diamond", user_info.diamond.GetDecrypted());
        param.Add("point", user_info.point.GetDecrypted());
        param.Add("power", user_info.power.GetDecrypted());
        param.Add("user_unit", Update_list_converter(user_info.user_unit));
        param.Add("select_unit", user_info.select_unit.GetDecrypted());
        param.Add("user_stage", user_info.user_stage.GetDecrypted());
        param.Add("tresure_box_reward_time", user_info.tresure_box_reward_time);
        param.Add("review", user_info.review.GetDecrypted().ToString());
        param.Add("notice_time", user_info.notice_time);
        param.Add("save_gold_time", user_info.save_gold_time);
        param.Add("user_level", user_info.user_level.GetDecrypted());
        param.Add("user_skill_point", user_info.user_skill_point.GetDecrypted());
        param.Add("user_experience", user_info.user_experience.GetDecrypted());
        param.Add("user_skill", Update_list_converter(user_info.user_skill));
        param.Add("user_item_time", Update_list_string_converter(user_info.user_item_time));
        param.Add("user_item_ad_time", Update_list_string_converter(user_info.user_item_ad_time));
        param.Add("user_item", Update_list_converter(user_info.user_item));
        param.Add("achievements", Update_list_converter(user_info.user_achievements));
        param.Add("monster_count", user_info.monster_count.GetDecrypted());
        param.Add("boss_count", user_info.boss_count.GetDecrypted());
        param.Add("ad_count", user_info.ad_count.GetDecrypted());
        param.Add("best_stage", user_info.best_stage.GetDecrypted());
        param.Add("total_stage", user_info.total_stage.GetDecrypted());
        param.Add("tresure_box_count", user_info.tresure_box_count.GetDecrypted());
        param.Add("total_again", user_info.total_again.GetDecrypted());
        param.Add("user_quest_value", Update_list_converter(user_info.user_quest_value));
        param.Add("user_quest_reward", Update_list_converter(user_info.user_quest_reward));
        param.Add("attendance_check", user_info.attendance_check.GetDecrypted());
        param.Add("attendance_count", user_info.attendance_count.GetDecrypted());
        param.Add("attendance_level", user_info.attendance_level.GetDecrypted());
        param.Add("attendance_time", user_info.attendance_time.ToString());
        param.Add("attendance_reward", Update_list_converter(user_info.attendance_reward));
        param.Add("attendance_special_reward", Update_list_converter(user_info.attendance_special_reward));
        param.Add("upgrade_gold", user_info.upgrade_gold_attack_damage.ToString() + "," + user_info.upgrade_gold_attack_speed.ToString() + "," + user_info.upgrade_gold_critical_damage.ToString() + "," + user_info.upgrade_gold_critical_percent.ToString() + "," + user_info.upgrade_gold_life.ToString() + "," + user_info.upgrade_gold_enemy_life.ToString() + "," + user_info.upgrade_gold_again.ToString());
        param.Add("upgrade_diamond", user_info.upgrade_diamond_attack_damage.ToString() + "," + user_info.upgrade_diamond_attack_speed.ToString() + "," + user_info.upgrade_diamond_critical_damage.ToString() + "," + user_info.upgrade_diamond_critical_percent.ToString() + "," + user_info.upgrade_diamond_life.ToString() + "," + user_info.upgrade_diamond_enemy_life.ToString() + "," + user_info.upgrade_diamond_again.ToString());
        param.Add("upgrade_point", user_info.upgrade_point_attack_damage.ToString() + "," + user_info.upgrade_point_attack_speed.ToString() + "," + user_info.upgrade_point_critical_damage.ToString() + "," + user_info.upgrade_point_critical_percent.ToString() + "," + user_info.upgrade_point_life.ToString() + "," + user_info.upgrade_point_enemy_life.ToString() + "," + user_info.upgrade_point_again.ToString());
        param.Add("pvp_time", user_info.pvp_time.ToString());
        param.Add("pvp_end_time", user_info.pvp_end_time.ToString());
        param.Add("pvp_ticket", user_info.pvp_ticket.GetDecrypted().ToString());
        param.Add("pvp_ad_time", user_info.pvp_ad_time.ToString());
        param.Add("free_diamond_ticket", user_info.free_diamond_ticket.GetDecrypted().ToString());
        param.Add("free_diamond_ad_time", user_info.free_diamond_ad_time.ToString());
        param.Add("point_treasure_ticket", user_info.point_treasure_ticket.GetDecrypted().ToString());
        param.Add("package_active", user_info.package_active.GetDecrypted().ToString());
        param.Add("package_active_time", user_info.package_active_time.ToString());
        param.Add("start_package", user_info.start_package.ToString());
        param.Add("gold_start_package", user_info.gold_start_package.ToString());
        param.Add("user_compose_count", Update_list_converter(user_info.compose_stack));
        param.Add("auto_again_active", user_info.auto_again_active.ToString());
        param.Add("auto_again_setting", user_info.auto_again_setting.ToString());
        param.Add("auto_again_stage", user_info.auto_again_stage.ToString());
        param.Add("auto_again_time", user_info.auto_again_time.ToString());

        //skin
        param.Add("user_skin_state", user_info.user_skin_state.ToString());
        param.Add("user_skin_list", Update_list_converter(user_info.user_skin_list));
        param.Add("user_current_skin", user_info.user_current_skin.ToString());

        //간식 패키지
        param.Add("item_package_purchase", user_info.item_package_purchase.ToString());
        param.Add("item_package_time", user_info.item_package_time.ToString());

        //아이템 ,룬 광고 횟수 제한
        param.Add("user_item_power_ad_count", user_info.user_item_power_ad_count.ToString());
        param.Add("user_item_speed_ad_count", user_info.user_item_speed_ad_count.ToString());
        param.Add("user_rune_ad_count", user_info.user_rune_ad_count.ToString());

        //마지막으로 접속 유지한 날짜
        param.Add("last_connected_time", user_info.last_connected_time);

        //미트 개편 이후 새로운 스타트 패키지
        param.Add("new_start_package", user_info.new_start_package.ToString());
        param.Add("new_gold_start_package", user_info.new_gold_start_package.ToString());

        //var param = Param.Parse(user_info);
        // Debug.Log(param.GetJson());

        Backend.GameData.Update("user_info", user_info.inDate, param, (callback) =>
        {
            // Debug.Log(param.GetJson());
            if (callback.GetStatusCode() == "204")
            {
                /// Debug.Log("------------------------user_info_update success");

                if (SceneManager.GetActiveScene().name == "GameScene")
                {
                    UIManager.Instance.top_panel.User_gold_setting();
                    UIManager.Instance.game_panel.User_item_setting();
                }
            }
            else
            {
                ServerManager.Instance.error_type = 14;
                ServerManager.Instance.server_loading = false;
                ServerManager.Instance.Server_connect_check();
                ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString(), param.GetJson());
            }

        });
    }
    public void Update_user_info_pvp()
    {
        Debug.Log("-Update_user_info_pvp-");
        Param param = new Param();
        param.Add("pvp_point", user_info.pvp_point.GetDecrypted());
        param.Add("pvp_end_time", user_info.pvp_end_time.ToString());

        Backend.GameData.Update("user_info", user_info.inDate, param, (callback) =>
        {

            if (callback.IsSuccess())
            {
                Debug.Log("------------------------user_info_update success");
            }
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                UIManager.Instance.top_panel.User_gold_setting();
                UIManager.Instance.game_panel.User_item_setting();
            }
        });
    }
    public void DB_Insert_pvp_user_info(JsonData json)
    {
        Debug.Log("-DB_Insert_pvp_user_info-");

        pvp_user_info.indate = json[1][0]["inDate"]["S"].ToString();
        pvp_user_info.attack_damage = int.Parse(json[1][0]["attack_damage"]["N"].ToString());
        pvp_user_info.attack_speed = float.Parse(json[1][0]["attack_speed"]["N"].ToString());
        pvp_user_info.critical_damage = int.Parse(json[1][0]["critical_damage"]["N"].ToString());
        pvp_user_info.critical_percent = float.Parse(json[1][0]["critical_percent"]["N"].ToString());
        pvp_user_info.life = int.Parse(json[1][0]["life"]["N"].ToString());

        if (pvp_user_info.indate == null)
        {

        }

        ServerManager.Instance.get_pvp_user_info_table_check = true;
    }
    public void DB_Insert_game_info(JsonData json)
    {
        Debug.Log("DB_Insert_game_info");
        char[] splitter_1 = { ',' };
        char[] splitter_2 = { '/' };

        string[] splitter_gm = json["rows"][0]["gm"]["S"].ToString().Split(splitter_1);
        string[] splitter_exp = json["rows"][0]["exp"]["S"].ToString().Split(splitter_1);
        string[] splitter_user_skill_level_value_temp = json["rows"][0]["user_skill_level_value"]["S"].ToString().Split(splitter_2);
        string[] splitter_tip_temp = json["rows"][0]["tip"]["S"].ToString().Split(splitter_2);
        string[] splitter_tip = splitter_tip_temp[DBManager.Instance.language_index].ToString().Split(splitter_1);

        for (int i = 0; i < splitter_gm.Length; i++)
        {
            // Debug.Log("경험치 버그 테스트  " + splitter_target.Length + "===================" + i );
            gm.Add(splitter_gm[i]);
        }

        for (int i = 0; i < splitter_tip.Length; i++)
        {
            // Debug.Log("경험치 버그 테스트  " + splitter_target.Length + "===================" + i );
            list_tip.Add(splitter_tip[i]);
        }

        // Debug.Log("경험치 버그 테스트 " + splitter_target.Length);
        for (int i = 0; i < splitter_exp.Length; i++)
        {
            // Debug.Log("경험치 버그 테스트  " + splitter_target.Length + "===================" + i );
            exp.Add(int.Parse(splitter_exp[i]));
        }
        for (int i = 0; i < splitter_user_skill_level_value_temp.Length; i++)
        {
            List<int> user_skill_temp = new List<int>();
            string[] splitter_user_skill_level_value = splitter_user_skill_level_value_temp[i].ToString().Split(splitter_1);
            for (int ii = 0; ii < splitter_user_skill_level_value.Length; ii++)
            {
                user_skill_temp.Add(int.Parse(splitter_user_skill_level_value[ii]));
            }
            user_skill_value.Add(user_skill_temp);
        }

        google_store_url = json[1][0]["google_store_url"]["S"].ToString();
        apple_store_url = json[1][0]["apple_store_url"]["S"].ToString();

        if (ServerManager.Instance.oneStore == true)
        {
            OneStore.Instance.base64EncodedPublicKey = json[1][0]["onestore"]["S"].ToString();
            OneStore.Instance.On_StartCoroutine();
        }
        ServerManager.Instance.gm = gm_check();
        ServerManager.Instance.get_game_info_table_check = true;
    }
    public bool gm_check()
    {
        return gm.Contains(user_info.nickname);
    }
    public void DB_Insert_ranking_post_info(JsonData json)
    {
        Debug.Log("-DB_Insert_ranking_post_info-");

        char[] splitter = { ',' };

        for (int i = 0; i < json[0].Count; i++)
        {
            Post_info post_temp = new Post_info();
            post_temp.post_indate = json[0][i]["inDate"]["S"].ToString();
            post_temp.post_type = "rank";
            post_temp.post_send_time = json[0][i]["sentDate"]["S"].ToString();
            string[] splitter_post_title = json[0][i]["item"]["M"]["title"]["S"].ToString().Split(splitter);
            string[] splitter_post_body = json[0][i]["item"]["M"]["body"]["S"].ToString().Split(splitter);
            post_temp.post_title = splitter_post_title[language_index];
            post_temp.post_body = splitter_post_body[language_index];
            post_temp.post_reward_type = int.Parse(json[0][i]["item"]["M"]["type"]["S"].ToString());
            post_temp.post_reward_value = int.Parse(json[0][i]["item"]["M"]["value"]["S"].ToString());
            post_temp.post_indate = json[0][i]["inDate"]["S"].ToString();
            post_temp.post_expiration = json[0][i]["expirationDate"]["S"].ToString();
            // Debug.Log("ddddddddddddddddddddddddddddddddddddddddddddddddd" + json[0][i].Count);
            list_post_info.Add(post_temp);
        }
    }
    public void DB_Insert_admin_post_info(JsonData json)
    {
        char[] splitter = { ',' };
        for (int i = 0; i < json[0].Count; i++)
        {
            Post_info post_temp = new Post_info();
            post_temp.post_indate = json[0][i]["inDate"]["S"].ToString(); // 우편 인데이트
            post_temp.post_type = "admin";
            post_temp.post_send_time = json[0][i]["sentDate"]["S"].ToString(); // 보낸시간
            post_temp.post_reward_type = int.Parse(json[0][i]["item"]["M"]["type"]["S"].ToString()); // 우편 보상 아이템
            post_temp.post_reward_value = int.Parse(json[0][i]["item"]["M"]["value"]["S"].ToString()); // 개수
            //post_temp.post_reward_type = int.Parse(json[0][i]["item"]["M"]["post_type"]["S"].ToString()); // 우편 보상 아이템
            //post_temp.post_reward_value = int.Parse(json[0][i]["item"]["M"]["post_value"]["S"].ToString()); // 개수
            string[] splitter_post_title = json[0][i]["title"]["S"].ToString().Split(splitter); // 우편 제목
            string[] splitter_post_body = json[0][i]["content"]["S"].ToString().Split(splitter); // 우편 내용
            if (splitter_post_title.Length > 1)
            {
                post_temp.post_title = splitter_post_title[language_index];
                post_temp.post_body = splitter_post_body[language_index];
            }
            else
            {
                post_temp.post_title = splitter_post_title[0];
                post_temp.post_body = splitter_post_body[0];
            }

            post_temp.post_indate = json[0][i]["inDate"]["S"].ToString();// ??
            post_temp.post_expiration = json[0][i]["expirationDate"]["S"].ToString(); // 만료 일시
            //  Debug.Log("ddddddddddddddddddddddddddddddddddddddddddddddddd" + json[0][i].Count);
            list_post_info.Add(post_temp);
        }

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            ServerManager.Instance.server_loading = false;
            UIManager.Instance.post_panel.Post_panel_setting();
        }
    }
    public void Send_post_reward(JsonData json)
    {
        int item_type = int.Parse(json[0]["M"]["type"]["S"].ToString());
        int item_value = int.Parse(json[0]["M"]["value"]["S"].ToString());
        if (item_type == 0)
        {
            user_info.diamond += item_value;
            DB_Update_user_info();
            UIManager.Instance.quest_gold_particle(UIManager.Instance.post_panel.transform);
        }
        else if (item_type == 1)
        {
            user_info.diamond += item_value;
            DB_Update_user_info();
            UIManager.Instance.quest_gold_particle(UIManager.Instance.post_panel.transform);
        }
        else if (item_type == 2)
        {
            user_info.diamond += item_value;
            DB_Update_user_info();
            UIManager.Instance.quest_gold_particle(UIManager.Instance.post_panel.transform);
        }
        //UIManager.Instance.post_panel.Post_panel_setting();
        //ServerManager.Instance.server_loading = false;
    }
    public string Update_list_converter(List<int> list)
    {
        string temp = "";

        for (int i = 0; i < list.Count; i++)
        {
            temp += list[i].ToString();
            if (i < list.Count - 1)
                temp += ",";
            //Debug.Log(i+"-------------------------Update_user_unit_converter-----" + list[i].ToString());
        }

        return temp;
    }
    public string Update_list_converter(List<List<int>> list)
    {
        string temp = "";

        for (int i = 0; i < list.Count; i++)
        {
            for (int ii = 0; ii < list[i].Count; ii++)
            {
                temp += list[i][ii].ToString();
                if (ii < list[i].Count - 1)
                    temp += ",";

            }
            if (i < list.Count - 1)
                temp += "/";
            //Debug.Log(i+"-------------------------Update_user_unit_converter-----" + list[i].ToString());
        }

        return temp;
    }
    public string Update_list_converter(List<ObscuredInt> list)
    {
        string temp = "";

        for (int i = 0; i < list.Count; i++)
        {
            temp += list[i].ToString();
            if (i < list.Count - 1)
                temp += ",";
            //Debug.Log(i+"-------------------------Update_user_unit_converter-----" + list[i].ToString());
        }

        return temp;
    }
    public string Update_list_converter(List<List<ObscuredInt>> list)
    {
        string temp = "";

        for (int i = 0; i < list.Count; i++)
        {
            for (int ii = 0; ii < list[i].Count; ii++)
            {
                temp += list[i][ii].ToString();
                if (ii < list[i].Count - 1)
                    temp += ",";
            }
            if (i < list.Count - 1)
                temp += "/";
        }
        //Debug.Log("pet == temp" + temp);
        return temp;
    }
    public string Update_list_long_converter(List<long> list)
    {
        string temp = "";

        for (int i = 0; i < list.Count; i++)
        {
            temp += list[i].ToString();
            if (i < list.Count - 1)
                temp += ",";
            //Debug.Log(i+"-------------------------Update_user_unit_converter-----" + list[i].ToString());
        }

        return temp;
    }
    public string Update_list_long_converter(List<ObscuredLong> list)
    {
        string temp = "";

        for (int i = 0; i < list.Count; i++)
        {
            temp += list[i].ToString();
            if (i < list.Count - 1)
                temp += ",";
            //Debug.Log(i+"-------------------------Update_user_unit_converter-----" + list[i].ToString());
        }

        return temp;
    }
    public string Update_list_string_converter(List<string> list)
    {
        string temp = "";

        for (int i = 0; i < list.Count; i++)
        {
            temp += list[i].ToString();
            if (i < list.Count - 1)
                temp += ",";
            //Debug.Log(i+"-------------------------Update_user_unit_converter-----" + list[i].ToString());
        }

        return temp;
    }
    public string Update_list_string_converter(List<ObscuredString> list)
    {
        string temp = "";

        for (int i = 0; i < list.Count; i++)
        {
            temp += list[i].ToString();
            if (i < list.Count - 1)
                temp += ",";
            //Debug.Log(i+"-------------------------Update_user_unit_converter-----" + list[i].ToString());
        }

        return temp;
    }

    public void DB_inster_user_rank(JsonData json, string type)
    {
        if (type == "power")
        {
            PhotonManager.Instance.userName = json[0][0]["nickname"].ToString();
            PhotonManager.Instance.userRank = json[0][0]["rank"]["N"].ToString();
            user_power.rank = int.Parse(json[0][0]["rank"]["N"].ToString());
            user_power.nick_name = json[0][0]["nickname"].ToString();
            user_power.value = long.Parse(json[0][0]["score"]["N"].ToString());
        }
        else if (type == "total_stage")
        {
            user_total_stage.rank = int.Parse(json[0][0]["rank"]["N"].ToString());
            user_total_stage.nick_name = json[0][0]["nickname"].ToString();
            user_total_stage.value = int.Parse(json[0][0]["score"]["N"].ToString());
        }
        else if (type == "best_stage")
        {
            user_best_stage.rank = int.Parse(json[0][0]["rank"]["N"].ToString());
            user_best_stage.nick_name = json[0][0]["nickname"].ToString();
            user_best_stage.value = int.Parse(json[0][0]["score"]["N"].ToString());
        }
        else if (type == "pvp_point")
        {
            user_pvp.rank = int.Parse(json[0][0]["rank"]["N"].ToString());
            user_pvp.nick_name = json[0][0]["nickname"].ToString();
            user_pvp.value = int.Parse(json[0][0]["score"]["N"].ToString());
        }

    }
    public void DB_insert_pvp_enemy(JsonData json, string nickname, string power)
    {

        pvp_enemy_info.unit_index = int.Parse(json["rows"][0]["unit"]["N"].ToString());
        pvp_enemy_info.nick_name = nickname;
        pvp_enemy_info.power = long.Parse(power);
        pvp_enemy_info.attack_damage = int.Parse(json["rows"][0]["attack_damage"]["N"].ToString());
        pvp_enemy_info.attack_speed = float.Parse(json["rows"][0]["attack_speed"]["N"].ToString());
        pvp_enemy_info.critical_damage = int.Parse(json["rows"][0]["critical_damage"]["N"].ToString());
        pvp_enemy_info.critical_percent = float.Parse(json["rows"][0]["critical_percent"]["N"].ToString());
        pvp_enemy_info.life = int.Parse(json["rows"][0]["life"]["N"].ToString());

        Debug.LogWarning("DBinsertpvpenemy");
        UIManager.Instance.pvp_panel.pvp_info_panel_setting();
    }
    public void DB_Insert_user_ranking(JsonData json)
    {
        Debug.Log("----------------------DB_Insert_user_ranking--------------------------");
        if (json["detailType"].ToString() == "best_stage")
        {
            Debug.Log("best_stage");
            list_best_stage.Clear();
            for (int i = 0; i < json[0].Count; i++)
            {
                User_ranking ranking_temp = new User_ranking();
                ranking_temp.nick_name = json[0][i]["nickname"].ToString();
                ranking_temp.rank = int.Parse(json[0][i]["rank"]["N"].ToString());
                ranking_temp.value = int.Parse(json[0][i]["score"]["N"].ToString());
                ranking_temp.InDate = json[0][i]["gamerInDate"].ToString();
                ranking_temp.type = "best";

                list_best_stage.Add(ranking_temp);
            }
        }
        else if (json["detailType"].ToString() == "total_stage")
        {
            Debug.Log("total_stage");
            list_total_stage.Clear();
            for (int i = 0; i < json[0].Count; i++)
            {
                User_ranking ranking_temp = new User_ranking();
                ranking_temp.nick_name = json[0][i]["nickname"].ToString();
                ranking_temp.rank = int.Parse(json[0][i]["rank"]["N"].ToString());
                ranking_temp.value = int.Parse(json[0][i]["score"]["N"].ToString());
                ranking_temp.InDate = json[0][i]["gamerInDate"].ToString();
                ranking_temp.type = "total";

                list_total_stage.Add(ranking_temp);
            }
        }
        else if (json["detailType"].ToString() == "total_again")
        {
            Debug.Log("total_again");
            list_total_again.Clear();
            for (int i = 0; i < json[0].Count; i++)
            {
                User_ranking ranking_temp = new User_ranking();
                ranking_temp.nick_name = json[0][i]["nickname"].ToString();
                ranking_temp.rank = int.Parse(json[0][i]["rank"]["N"].ToString());
                ranking_temp.value = int.Parse(json[0][i]["score"]["N"].ToString());

                list_total_again.Add(ranking_temp);
            }
        }
        else if (json["detailType"].ToString() == "pvp_point")
        {
            Debug.Log("pvp_point");
            user_info.pvp_end_time = json["period"]["endDate"].ToString();

            UIManager.Instance.pvp_panel.pvp_end_time = json["period"]["endDate"].ToString();

            list_pvp.Clear();
            for (int i = 0; i < json[0].Count; i++)
            {
                User_ranking ranking_temp = new User_ranking();
                ranking_temp.nick_name = json[0][i]["nickname"].ToString();
                ranking_temp.rank = int.Parse(json[0][i]["rank"]["N"].ToString());
                ranking_temp.value = int.Parse(json[0][i]["score"]["N"].ToString());

                list_pvp.Add(ranking_temp);
            }
        }
        else if (json["detailType"].ToString() == "power")
        {
            Debug.Log("power");
            list_power.Clear();
            Debug.Log(json[0].Count);
            for (int i = 0; i < json[0].Count; i++)
            {
                User_ranking ranking_temp = new User_ranking();
                ranking_temp.nick_name = json[0][i]["nickname"].ToString();
                ranking_temp.rank = int.Parse(json[0][i]["rank"]["N"].ToString());
                ranking_temp.value = long.Parse(json[0][i]["score"]["N"].ToString());
                ranking_temp.InDate = json[0][i]["gamerInDate"].ToString();
                ranking_temp.type = "power";

                list_power.Add(ranking_temp);
            }
        }
    }

    public void DB_insert_ranking_match_user_rank(JsonData ranking_match_rank)
    {
        Debug.Log("----- DB_insert_ranking_match_user_rank -----");

        list_ranking_match.Clear();

        for (int i = 0; i < ranking_match_rank["rows"].Count; i++)
        {
            User_ranking ranking_match_temp = new User_ranking();
            ranking_match_temp.nick_name = ranking_match_rank["rows"][i]["nickname"]["S"].ToString();
            ranking_match_temp.rank = int.Parse(ranking_match_rank["rows"][i]["rank"]["N"].ToString());
            ranking_match_temp.value = long.Parse(ranking_match_rank["rows"][i]["score"]["N"].ToString());
            ranking_match_temp.InDate = ranking_match_rank["rows"][i]["gamerInDate"]["S"].ToString();
            ranking_match_temp.type = "ranking_match";

            list_ranking_match.Add(ranking_match_temp);
            //Debug.LogWarning(i);
        }
    }

    public void DB_insert_ranking_match_my_rank(JsonData user_ranking_match_rank)
    {
        Debug.Log("----- DB_insert_ranking_match_user_rank -----");

        user_ranking_match.InDate = user_ranking_match_rank["rows"][0]["gamerInDate"]["S"].ToString();
        user_ranking_match.nick_name = user_ranking_match_rank["rows"][0]["nickname"]["S"].ToString();
        user_ranking_match.rank = int.Parse(user_ranking_match_rank["rows"][0]["rank"]["N"].ToString());
        user_ranking_match.value = long.Parse(user_ranking_match_rank["rows"][0]["score"]["N"].ToString());
        user_ranking_match.type = "ranking_match";
    }

    // 스테이지 InDate 가져오기
    //1층 부터 100층 까지 입주자가 누구냐
    public void DB_insert_rank_info(JsonData rank_json, string rank)
    {
        Debug.Log("----- DB_insert_rank_info -----");

        user_ranking_match_info.rank_InDate.Clear();

        if (rank.Equals("Power"))
        {
            Debug.Log("Power");
            for (int i = 0; i < rank_json["rows"].Count; i++)
            {
                user_ranking_match_info.rank_InDate.Add(rank_json["rows"][i]["gamerInDate"].ToString());
            }
        }
        else if (rank.Equals("Ranking match"))
        {
            Debug.Log("Ranking match rank");
            for (int i = 0; i < rank_json["rows"].Count; i++)
            {
                user_ranking_match_info.rank_InDate.Add(rank_json["rows"][i]["gamerInDate"]["S"].ToString());
            }
        }
    }

    // 랭킹전 스테이지 정보
    // 그 입주자의 mbti
    public void DB_insert_ranking_match_stage_info(JsonData user_pvp_json, int stage_number)
    {
        Debug.Log("----- DB_insert_ranking_match_stage_info -----");

        user_ranking_match_info.stage_nickname = user_pvp_json[1][0]["nickname"]["S"].ToString();
        user_ranking_match_info.stage_dog_index = int.Parse(user_pvp_json[1][0]["unit"]["N"].ToString());
        user_ranking_match_info.stage_atteck_damage = int.Parse(user_pvp_json[1][0]["attack_damage"]["N"].ToString());
        user_ranking_match_info.stage_atteck_speed = float.Parse(user_pvp_json[1][0]["attack_speed"]["N"].ToString());
        user_ranking_match_info.stage_critical_damage = int.Parse(user_pvp_json[1][0]["critical_damage"]["N"].ToString());
        user_ranking_match_info.stage_critical_percent = float.Parse(user_pvp_json[1][0]["critical_percent"]["N"].ToString());
        if (user_ranking_match_info.current_stage < 10)
        {
            user_ranking_match_info.stage_life = (long)(long.Parse(user_pvp_json[1][0]["life"]["N"].ToString()) * (double.Parse(user_pvp_json[1][0]["attack_damage"]["N"].ToString()) * 0.01));
        }
        else if (user_ranking_match_info.current_stage >= 10 && user_ranking_match_info.current_stage < 20)
        {
            user_ranking_match_info.stage_life = (long)(long.Parse(user_pvp_json[1][0]["life"]["N"].ToString()) * (double.Parse(user_pvp_json[1][0]["attack_damage"]["N"].ToString()) * 0.03));
        }
        else if (user_ranking_match_info.current_stage >= 20 && user_ranking_match_info.current_stage < 30)
        {
            user_ranking_match_info.stage_life = (long)(long.Parse(user_pvp_json[1][0]["life"]["N"].ToString()) * (double.Parse(user_pvp_json[1][0]["attack_damage"]["N"].ToString()) * 0.05));
        }
        else if (user_ranking_match_info.current_stage >= 30 && user_ranking_match_info.current_stage < 40)
        {
            user_ranking_match_info.stage_life = (long)(long.Parse(user_pvp_json[1][0]["life"]["N"].ToString()) * (double.Parse(user_pvp_json[1][0]["attack_damage"]["N"].ToString()) * 0.07));
        }
        else if (user_ranking_match_info.current_stage >= 40 && user_ranking_match_info.current_stage < 50)
        {
            user_ranking_match_info.stage_life = (long)(long.Parse(user_pvp_json[1][0]["life"]["N"].ToString()) * (double.Parse(user_pvp_json[1][0]["attack_damage"]["N"].ToString()) * 0.09));
        }
        else if (user_ranking_match_info.current_stage >= 50 && user_ranking_match_info.current_stage < 60)
        {
            user_ranking_match_info.stage_life = (long)(long.Parse(user_pvp_json[1][0]["life"]["N"].ToString()) * (double.Parse(user_pvp_json[1][0]["attack_damage"]["N"].ToString()) * 0.11));
        }
        else if (user_ranking_match_info.current_stage >= 60 && user_ranking_match_info.current_stage < 70)
        {
            user_ranking_match_info.stage_life = (long)(long.Parse(user_pvp_json[1][0]["life"]["N"].ToString()) * (double.Parse(user_pvp_json[1][0]["attack_damage"]["N"].ToString()) * 0.13));
        }
        else if (user_ranking_match_info.current_stage >= 70 && user_ranking_match_info.current_stage < 80)
        {
            user_ranking_match_info.stage_life = (long)(long.Parse(user_pvp_json[1][0]["life"]["N"].ToString()) * (double.Parse(user_pvp_json[1][0]["attack_damage"]["N"].ToString()) * 0.15));
        }
        else if (user_ranking_match_info.current_stage >= 80 && user_ranking_match_info.current_stage < 90)
        {
            user_ranking_match_info.stage_life = (long)(long.Parse(user_pvp_json[1][0]["life"]["N"].ToString()) * (double.Parse(user_pvp_json[1][0]["attack_damage"]["N"].ToString()) * 0.175));
        }
        else if (user_ranking_match_info.current_stage >= 90 && user_ranking_match_info.current_stage < 99)
        {
            user_ranking_match_info.stage_life = (long)(long.Parse(user_pvp_json[1][0]["life"]["N"].ToString()) * (double.Parse(user_pvp_json[1][0]["attack_damage"]["N"].ToString()) * 0.2));
        }
        else if (user_ranking_match_info.current_stage == 99)
        {
            user_ranking_match_info.stage_life = (long)(long.Parse(user_pvp_json[1][0]["life"]["N"].ToString()) * (double.Parse(user_pvp_json[1][0]["attack_damage"]["N"].ToString()) * 0.4));
        }

        user_ranking_match_info.stage_power = (long)((long.Parse(user_pvp_json[1][0]["attack_damage"]["N"].ToString()) * float.Parse(user_pvp_json[1][0]["attack_speed"]["N"].ToString()) / 60) + (long.Parse(user_pvp_json[1][0]["critical_damage"]["N"].ToString()) * float.Parse(user_pvp_json[1][0]["critical_percent"]["N"].ToString()) / 10f) + long.Parse(user_pvp_json[1][0]["life"]["N"].ToString()));
        user_ranking_match_info.stage_pet_index = int.Parse(user_pvp_json[1][0]["pet_index"]["N"].ToString());
        user_ranking_match_info.stage_pet_type = int.Parse(user_pvp_json[1][0]["pet_type"]["N"].ToString());

        if (ServerManager.Instance.ranking_match_stage_life_set == true)
        {
            user_ranking_match_info.stage_current_remaining_life = user_ranking_match_info.stage_life;
            ServerManager.Instance.ranking_match_stage_life_set = false;
        }
    }
    // 유저 랭킹전 정보
    public void DB_insert_user_ranking_match_info(JsonData json)
    {
        Debug.Log("----- DB_insert_user_ranking_match_info IN -----");
        user_ranking_match_info.InDate = json[1][0]["owner_inDate"]["S"].ToString();
        user_ranking_match_info.current_stage = int.Parse(json[1][0]["current_stage"]["N"].ToString());
        user_ranking_match_info.user_nickname = DBManager.Instance.user_info.nickname;
        user_ranking_match_info.ranking_match_point = int.Parse(json[1][0]["ranking_match_point"]["N"].ToString());
        user_ranking_match_info.ranking_match_ticket = int.Parse(json[1][0]["ranking_match_ticket"]["N"].ToString());
        user_ranking_match_info.ranking_match_ticket_use_time = json[1][0]["ranking_match_ticket_use_time"]["S"].ToString();
        user_ranking_match_info.ranking_match_buy_ticket = int.Parse(json[1][0]["ranking_match_buy_ticket"]["N"].ToString());
        user_ranking_match_info.ranking_match_end_time = json[1][0]["ranking_match_end_time"]["S"].ToString();
        user_ranking_match_info.stage_totalplaytime = int.Parse(json[1][0]["stage_totalplaytime"]["N"].ToString());
        user_ranking_match_info.stage_current_remaining_life = long.Parse(json[1][0]["stage_current_remaining_life"]["N"].ToString());

        string[] ranking_match_stage_reward_check = json[1][0]["stage_reward_check"]["S"].ToString().Split(',');
        for (int i = 0; i < 100; i++)
        {
            user_ranking_match_info.stage_reward_check.Add(int.Parse(ranking_match_stage_reward_check[i]));
        }

        string[] ranking_match_stage_InDate = json[1][0]["stage_inDate"]["S"].ToString().Split(',');
        for (int i = 0; i < 100; i++)
        {
            user_ranking_match_info.rank_InDate.Add(ranking_match_stage_InDate[i]);
        }

        IDictionary dictionary = json[1][0] as IDictionary;
        if (dictionary.Contains("is_get_stage"))
        {
            user_ranking_match_info.is_get_stage = int.Parse(json[1][0]["is_get_stage"]["N"].ToString());
        }
        else
        {
            user_ranking_match_info.is_get_stage = 0;
        }
    }

    // 랭킹전 정보 서버에 업데이트
    public void DB_Update_user_ranking_match_info()
    {
        Debug.Log("----- DB_Update_user_ranking_match_info -----");

        Param param = new Param();
        // 유저
        param.Add("current_stage", user_ranking_match_info.current_stage.GetDecrypted());
        param.Add("user_nickname", user_ranking_match_info.user_nickname);
        param.Add("ranking_match_end_time", user_ranking_match_info.ranking_match_end_time);
        param.Add("ranking_match_point", user_ranking_match_info.ranking_match_point.GetDecrypted());
        param.Add("ranking_match_ticket", user_ranking_match_info.ranking_match_ticket.GetDecrypted());
        param.Add("ranking_match_ticket_use_time", user_ranking_match_info.ranking_match_ticket_use_time);
        param.Add("ranking_match_buy_ticket", user_ranking_match_info.ranking_match_buy_ticket.GetDecrypted());
        param.Add("stage_totalplaytime", user_ranking_match_info.stage_totalplaytime.GetDecrypted());
        param.Add("stage_current_remaining_life", user_ranking_match_info.stage_current_remaining_life.GetDecrypted());
        param.Add("stage_reward_check", Update_list_converter(user_ranking_match_info.stage_reward_check));
        param.Add("stage_inDate", Update_list_string_converter(user_ranking_match_info.rank_InDate));
        param.Add("is_get_stage", user_ranking_match_info.is_get_stage);

        // 스테이지
        param.Add("stage_nickname", user_ranking_match_info.stage_nickname);
        param.Add("stage_dog_index", user_ranking_match_info.stage_dog_index.GetDecrypted());
        param.Add("stage_attack_damage", user_ranking_match_info.stage_atteck_damage.GetDecrypted());
        param.Add("stage_attack_speed", user_ranking_match_info.stage_atteck_speed.GetDecrypted());
        param.Add("stage_critical_damage", user_ranking_match_info.stage_critical_damage.GetDecrypted());
        param.Add("stage_critical_percent", user_ranking_match_info.stage_critical_percent.GetDecrypted());
        param.Add("stage_life", user_ranking_match_info.stage_life.GetDecrypted());
        param.Add("stage_pet_index", user_ranking_match_info.stage_pet_index.GetDecrypted());
        param.Add("stage_pet_type", user_ranking_match_info.stage_pet_type.GetDecrypted());


        Where where = new Where();
        where.Equal("owner_inDate", user_ranking_match_info.InDate);

        Backend.GameData.Update("Private_table_1", where, param, callback =>
        {
            if (callback.GetStatusCode() == "204")
            {
                Debug.Log("Update success");
            }
            else
            {
                ServerManager.Instance.error_type = 49;
                ServerManager.Instance.Server_connect_check();
                ServerManager.Instance.send_log_ranking_match_error(ServerManager.Instance.error_type, callback.GetStatusCode(), callback.GetMessage());
            }
        });
    }

    public void DB_Insert_quest_info(JsonData json) // 공용 퀘스트 데이터 DB 인서트
    {
        list_quest_info.Clear();
        for (int i = 0; i < json["rows"].Count; i++)
        {
            Quest_info quest_temp = new Quest_info();
            /*
            quest_temp.quest_index = int.Parse(json[1][i]["quest_index"]["S"].ToString());
            quest_temp.quest_name = json[1][i]["quest_name"]["S"].ToString();
            quest_temp.quest_reward = int.Parse(json[1][i]["quest_reward"]["S"].ToString());
            quest_temp.quest_target = int.Parse(json[1][i]["quest_target"]["S"].ToString());
            */

            quest_temp.quest_index = int.Parse(json["rows"][i]["quest_index"]["S"].ToString());
            quest_temp.quest_name = json["rows"][i]["quest_name"]["S"].ToString();
            quest_temp.quest_reward = int.Parse(json["rows"][i]["quest_reward"]["S"].ToString());
            quest_temp.quest_target = int.Parse(json["rows"][i]["quest_target"]["S"].ToString());

            list_quest_info.Add(quest_temp);
        }

        list_quest_info.Sort(delegate (Quest_info a, Quest_info b) { return a.quest_index.CompareTo(b.quest_index); });
        ServerManager.Instance.get_quest_table_check = true;
    }
    public void DB_Insert_achievement_info(JsonData json) // 공용 업적 데이터 DB 인서트
    {

        list_achievement_info.Clear();
        char[] splitter = { ',' };

        for (int i = 0; i < json[1].Count; i++)
        {
            Debug.Log("DB_Insert_achievement_info!!" + i);
            string[] splitter_reward = json[1][i]["reward"]["S"].ToString().Split(splitter);
            string[] splitter_target = json[1][i]["target"]["S"].ToString().Split(splitter);
            Achievement_info achievement_temp = new Achievement_info();
            achievement_temp.achievement_index = int.Parse(json[1][i]["index"]["S"].ToString());
            achievement_temp.achievement_name = json[1][i]["name"]["S"].ToString();
            achievement_temp.achievement_info = json[1][i]["info"]["S"].ToString();
            for (int s = 0; s < splitter_reward.Length; s++)
            {
                achievement_temp.list_target.Add(int.Parse(splitter_target[s].ToString()));
                achievement_temp.list_reward.Add(int.Parse(splitter_reward[s].ToString()));
            }
            list_achievement_info.Add(achievement_temp);
        }
        list_achievement_info.Sort(delegate (Achievement_info a, Achievement_info b) { return a.achievement_index.CompareTo(b.achievement_index); });
        ServerManager.Instance.get_achievement_table_check = true;
    }
    public void Language_parsing()
    {
        List<string> language_korean = new List<string>();
        List<string> language_english = new List<string>();
        List<string> language_chinese_1 = new List<string>();
        List<string> language_chinese_2 = new List<string>();
        List<string> language_japanese = new List<string>();
        textdata = Resources.Load("language") as TextAsset;
        JsonData jsondata = JsonMapper.ToObject(textdata.ToString());

        for (int i = 0; i < jsondata["korean"].Count; i++)
        {
            language_korean.Add(jsondata["korean"][i].ToString());
        }
        for (int i = 0; i < jsondata["english"].Count; i++)
        {
            language_english.Add(jsondata["english"][i].ToString());
        }
        for (int i = 0; i < jsondata["japanese"].Count; i++)
        {
            language_chinese_1.Add(jsondata["japanese"][i].ToString());
        }
        for (int i = 0; i < jsondata["chinese_1"].Count; i++)
        {
            language_chinese_2.Add(jsondata["chinese_1"][i].ToString());
        }
        for (int i = 0; i < jsondata["chinese_2"].Count; i++)
        {
            language_japanese.Add(jsondata["chinese_2"][i].ToString());
        }
        List_language.Add(language_korean);
        List_language.Add(language_english);
        List_language.Add(language_chinese_1);
        List_language.Add(language_chinese_2);
        List_language.Add(language_japanese);
    }

    public void DB_Insert_unit_info(JsonData json) // 공용 유닛 데이터 DB 인서트
    {
        // 차트
        for (int i = 0; i < json["rows"].Count; i++)
        {
            Unit_info unit_temp = new Unit_info();
            unit_temp.unit_index = int.Parse(json["rows"][i]["index"]["S"].ToString());
            unit_temp.grade = int.Parse(json["rows"][i]["grade"]["S"].ToString());
            unit_temp.name = Language_converter(200 + unit_temp.unit_index);
            unit_temp.info = Language_converter(250 + unit_temp.unit_index);
            unit_temp.attack_damage = int.Parse(json["rows"][i]["damage"]["S"].ToString());
            unit_temp.attack_speed = int.Parse(json["rows"][i]["speed"]["S"].ToString());
            unit_temp.critical_damage = int.Parse(json["rows"][i]["critical_damage"]["S"].ToString());
            unit_temp.critical_percent = float.Parse(json["rows"][i]["critical_percent"]["S"].ToString());
            unit_temp.life = int.Parse(json["rows"][i]["life"]["S"].ToString());
            unit_temp.ability_type = int.Parse(json["rows"][i]["ability_index"]["S"].ToString());
            unit_temp.ability_value = int.Parse(json["rows"][i]["ability_value"]["S"].ToString());
            if (unit_temp.grade == 0)
            {
                grade_1_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 1)
            {
                grade_2_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 2)
            {
                grade_3_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 3)
            {
                grade_4_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 4)
            {
                grade_5_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 5)
            {
                grade_6_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 6)
            {
                grade_7_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 7)
            {
                grade_8_unit.Add(unit_temp.unit_index);
            }
            list_unit_info.Add(unit_temp);
        }
        /*
        for (int i = 0; i < json[1].Count; i++)
        {
            Unit_info unit_temp = new Unit_info();
            unit_temp.unit_index = int.Parse(json[1][i]["index"]["S"].ToString());
            unit_temp.grade = int.Parse(json[1][i]["grade"]["S"].ToString());
            unit_temp.name = Language_converter(200 + unit_temp.unit_index);
            unit_temp.info = Language_converter(250 + unit_temp.unit_index);
            unit_temp.attack_damage = int.Parse(json[1][i]["damage"]["S"].ToString());
            unit_temp.attack_speed = int.Parse(json[1][i]["speed"]["S"].ToString());
            unit_temp.critical_damage = int.Parse(json[1][i]["critical_damage"]["S"].ToString());
            unit_temp.critical_percent = float.Parse(json[1][i]["critical_percent"]["S"].ToString());
            unit_temp.life = int.Parse(json[1][i]["life"]["S"].ToString());
            unit_temp.ability_type = int.Parse(json[1][i]["ability_index"]["S"].ToString());
            unit_temp.ability_value = int.Parse(json[1][i]["ability_value"]["S"].ToString());
            if (unit_temp.grade == 0)
            {
                grade_1_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 1)
            {
                grade_2_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 2)
            {
                grade_3_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 3)
            {
                grade_4_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 4)
            {
                grade_5_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 5)
            {
                grade_6_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 6)
            {
                grade_7_unit.Add(unit_temp.unit_index);
            }
            else if (unit_temp.grade == 7)
            {
                grade_8_unit.Add(unit_temp.unit_index);
            }
            list_unit_info.Add(unit_temp);
        }
        */
        list_unit_info.Sort(delegate (Unit_info a, Unit_info b) { return a.unit_index.CompareTo(b.unit_index); });
        ServerManager.Instance.get_unit_table_check = true;
    }
    public void DB_Insert_boss_info(JsonData json) // 공용 유닛 데이터 DB 인서트
    {
        Debug.Log("DB_Insert_boss_info");

        char[] splitter = { ',' };
        string[] splitter_boss_grade = json["rows"][0]["boss_grade"]["S"].ToString().Split(splitter);
        string[] splitter_boss_damage = json["rows"][0]["boss_damage"]["S"].ToString().Split(splitter);
        string[] splitter_boss_life = json["rows"][0]["boss_life"]["S"].ToString().Split(splitter);
        string[] splitter_boss_attack_speed = json["rows"][0]["boss_attack_speed"]["S"].ToString().Split(splitter);

        for (int i = 0; i < 5; i++)
        {
            Boss_info boss_temp = new Boss_info();
            boss_temp.boss_grade = int.Parse(splitter_boss_grade[i]);
            boss_temp.boss_damage = long.Parse(splitter_boss_damage[i]);
            boss_temp.boss_life = long.Parse(splitter_boss_life[i]);
            boss_temp.boss_attack_speed = int.Parse(splitter_boss_attack_speed[i]);

            list_boss_info.Add(boss_temp);
        }
        list_boss_info.Sort(delegate (Boss_info a, Boss_info b) { return a.boss_grade.CompareTo(b.boss_grade); });
        ServerManager.Instance.get_boss_info_table_check = true;
    }
    public void DB_Insert_user_boss_info(JsonData json)
    {
        Debug.Log("DB_Insert_user_boss_info");
        char[] splitter = { ',' };

        string[] splitter_boss_index = json[1][0]["boss_index"]["S"].ToString().Split(splitter);
        string[] splitter_boss_life = json[1][0]["boss_life"]["S"].ToString().Split(splitter);
        string[] splitter_boss_end_time = json[1][0]["boss_end_time"]["S"].ToString().Split(splitter);
        for (int s = 0; s < splitter_boss_index.Length; s++)
        {
            user_boss_info.boss_index.Add(int.Parse(splitter_boss_index[s]));
        }
        for (int s = 0; s < splitter_boss_life.Length; s++)
        {
            user_boss_info.boss_life.Add(long.Parse(splitter_boss_life[s]));
        }
        for (int s = 0; s < splitter_boss_end_time.Length; s++)
        {
            user_boss_info.boss_end_time.Add(splitter_boss_end_time[s]);
        }
        user_boss_info.inDate = json[1][0]["inDate"]["S"].ToString();

        user_boss_info.ticket_count = int.Parse(json[1][0]["boss_ticket"]["N"].ToString());
        user_boss_info.ticket_time = json[1][0]["boss_ticket_time"]["S"].ToString();

        //ServerManager.Instance.Check_boss_ticket();
        ServerManager.Instance.get_user_boss_info_table_check = true;
    }
    public void DB_Insert_user_rune_info(JsonData json)
    {
        char[] splitter = { ',' };

        string[] splitter_rune_level = json[1][0]["rune_level"]["S"].ToString().Split(splitter);
        string[] splitter_rune_stock = json[1][0]["rune_stock"]["S"].ToString().Split(splitter);

        user_rune_info.rune_level.Clear();
        user_rune_info.rune_stock.Clear();
        for (int s = 0; s < 4; s++)
        {
            user_rune_info.rune_level.Add(int.Parse(splitter_rune_level[s]));
        }
        for (int s = 0; s < 4; s++)
        {
            user_rune_info.rune_stock.Add(int.Parse(splitter_rune_stock[s]));
        }

        user_rune_info.inDate = json[1][0]["inDate"]["S"].ToString();

        user_rune_info.rune_stage = int.Parse(json[1][0]["rune_stage"]["S"].ToString());
        user_rune_info.rune_ticket = int.Parse(json[1][0]["rune_ticket"]["S"].ToString());
        user_rune_info.rune_time = json[1][0]["rune_time"]["S"].ToString();
        user_rune_info.rune_ad_time = json[1][0]["rune_ad_time"]["S"].ToString();

        //ServerManager.Instance.Check_boss_ticket();
        ServerManager.Instance.get_user_rune_info_table_check = true;
    }
    public void DB_Insert_user_pet_info(JsonData json)
    {
        Debug.Log("DB_Insert_user_pet_info");
        char[] splitter = { ',' };
        char[] splitter1 = { '/' };

        string[] splitter_user_pet_type = json[1][0]["user_pet"]["S"].ToString().Split(splitter1);


        user_pet_info.user_pet.Clear();
        for (int s = 0; s < splitter_user_pet_type.Length; s++)
        {
            string[] splitter_user_pet = splitter_user_pet_type[s].Split(splitter);
            List<ObscuredInt> pet_temp = new List<ObscuredInt>();
            for (int ss = 0; ss < splitter_user_pet.Length; ss++)
            {
                pet_temp.Add(int.Parse(splitter_user_pet[ss]));
            }
            user_pet_info.user_pet.Add(pet_temp);
        }

        user_pet_info.inDate = json[1][0]["inDate"]["S"].ToString();

        user_pet_info.pet_active = int.Parse(json[1][0]["pet_active"]["S"].ToString());
        user_pet_info.select_pet_index = int.Parse(json[1][0]["select_pet_index"]["S"].ToString());
        user_pet_info.select_pet_type = int.Parse(json[1][0]["select_pet_type"]["S"].ToString());

        //펫 각성 인서트
        IDictionary dictionary = json[1][0] as IDictionary;
        user_pet_info.pet_awakening_value.Clear();

        if (dictionary.Contains("pet_awakening_value"))
        {
            string[] splitter_user_pet_awakening_value = json[1][0]["pet_awakening_value"]["S"].ToString().Split(splitter);
            for (int i = 0; i < splitter_user_pet_awakening_value.Length; i++)
            {
                user_pet_info.pet_awakening_value.Add(int.Parse(splitter_user_pet_awakening_value[i]));
            }
        }
        else
        {
            user_pet_info.pet_awakening_value.Add(0);
            user_pet_info.pet_awakening_value.Add(0);
            user_pet_info.pet_awakening_value.Add(0);
            user_pet_info.pet_awakening_value.Add(0);
        }

        if (dictionary.Contains("select_pet_isAwakening"))
        {
            user_pet_info.select_pet_isAwakening = int.Parse(json[1][0]["select_pet_isAwakening"]["S"].ToString());
        }
        else
        {
            user_pet_info.select_pet_isAwakening = 0;
        }


        ServerManager.Instance.get_user_pet_info_table_check = true;
    }

    public void DB_Insert_user_event_info(JsonData json)
    {
        Debug.Log("--------------------DB_Insert_user_event_info-------------------------");
        char[] splitter = { ',' };

        string[] splitter_event_point_reward = json["rows"][0]["event_monster_check"]["S"].ToString().Split(splitter); //구매한 상품 횟수
        string[] splitter_event_point_limit = json["rows"][0]["event_monster_limit"]["S"].ToString().Split(splitter);        //삼품 구매 한도
        string[] splitter_event_point_price = json["rows"][0]["event_monster_price"]["S"].ToString().Split(splitter);       // 가격
        string[] splitter_package_check = json["rows"][0]["event_package_check"]["S"].ToString().Split(splitter);      // 패키지 상품 구매 여부
        string[] splitter_cherry_package_limit = json["rows"][0]["event_package_limit"]["S"].ToString().Split(splitter); // 패키지 구매 한도

        // 패키지 데이터가 예전 데이터면 수정
        if (splitter_package_check.Length != 5)
        {
            splitter_package_check = new string[] { "0", "0", "0", "0", "0" };
        }
        if (splitter_cherry_package_limit.Length != 5)
        {
            splitter_cherry_package_limit = new string[] { "5", "5", "5", "5", "5" };
        }

        user_event_info.inDate = json["rows"][0]["inDate"]["S"].ToString();

        user_event_info.event_monster_point = int.Parse(json["rows"][0]["event_monster_point"]["S"].ToString());


        IDictionary event_table_check = json["rows"][0] as IDictionary;

        //뒤끝 테이블에 칼럼 추가할 경우 기존 유저들은 해당 칼럼이 없기 때문에 조건문 검사로 체크 후 추가해줘야함
        if (event_table_check.Contains("event_monster_total"))
        {
            user_event_info.event_monster_total = int.Parse(json["rows"][0]["event_monster_total"]["S"].ToString()); // 최대 한도
        }
        else
        {
            user_event_info.event_monster_total = 8600;
        }

        user_event_info.event_monster_check.Clear();
        for (int i = 0; i < 7; i++)
        {
            user_event_info.event_monster_check.Add(int.Parse(splitter_event_point_reward[i]));
        }
        user_event_info.event_monster_limit.Clear();
        for (int i = 0; i < 7; i++)
        {
            user_event_info.event_monster_limit.Add(int.Parse(splitter_event_point_limit[i]));
        }

        user_event_info.event_monster_price.Clear();
        for (int i = 0; i < 7; i++)
        {
            user_event_info.event_monster_price.Add(int.Parse(splitter_event_point_price[i]));
        }
        user_event_info.event_package_check.Clear();
        for (int i = 0; i < 5; i++)
        {
            user_event_info.event_package_check.Add(int.Parse(splitter_package_check[i]));
        }

        user_event_info.event_package_limit.Clear();
        for (int i = 0; i < 5; i++)
        {
            user_event_info.event_package_limit.Add(int.Parse(splitter_cherry_package_limit[i]));
        }
    }
    public void DB_Insert_user_event_pay_info(JsonData json)
    {
        Debug.Log("--------------------DB_Insert_user_event_pay_info-------------------------");
        char[] splitter = { ',' };

        string[] splitter_event_pay_reward = json["rows"][0]["user_pay_reward_check"]["S"].ToString().Split(splitter);


        user_event_pay_info.inDate = json["rows"][0]["inDate"]["S"].ToString();

        user_event_pay_info.pay_point = int.Parse(json["rows"][0]["user_pay"]["S"].ToString());

        user_event_pay_info.pay_reward_check.Clear();

        IDictionary pay_reset_time_check = json["rows"][0] as IDictionary;

        if (pay_reset_time_check.Contains("event_reset_check"))
        {
            user_event_pay_info.event_reset_check = json["rows"][0]["event_reset_check"]["S"].ToString();
        }
        else
        {
            user_event_pay_info.event_reset_check = DateTime.Parse(ServerManager.Instance.Server_time_convert(JsonMapper.ToObject(Backend.Utils.GetServerTime().GetReturnValue()))).ToString("MM/yyyy");
        }

        for (int i = 0; i < splitter_event_pay_reward.Length; i++)
        {
            user_event_pay_info.pay_reward_check.Add(int.Parse(splitter_event_pay_reward[i]));
        }
        for (int i = 0; i < 9; i++)
            if (user_event_pay_info.pay_reward_check.Count < 9)
            {
                user_event_pay_info.pay_reward_check.Add(0);
                if (user_event_pay_info.pay_reward_check.Count >= 9)
                    break;
            }
    }
    public void DB_Insert_user_event_playTime_info(JsonData json)
    {
        Debug.Log("--------------------DB_Insert_user_event_playTime_info-------------------------");
        char[] splitter = { ',' };
        string[] splitter_event_playTime_reward = json["rows"][0]["user_playTime_reward_check"]["S"].ToString().Split(splitter);

        user_event_playTime_info.inDate = json["rows"][0]["inDate"]["S"].ToString();

        user_event_playTime_info.playTime = int.Parse(json["rows"][0]["user_playTime"]["S"].ToString());

        user_event_playTime_info.playTime_reward_check.Clear();
        for (int i = 0; i < 4; i++)
        {
            user_event_playTime_info.playTime_reward_check.Add(splitter_event_playTime_reward[i]);
        }
    }
    public void DB_Insert_user_event_mission_info(JsonData json)
    {
        Debug.Log("--------------------DB_Insert_user_event_mission_info-------------------------");
        char[] splitter = { ',' };

        string[] splitter_event_mission_value = json["rows"][0]["event_mission_value"]["S"].ToString().Split(splitter);
        string[] splitter_event_mission_check = json["rows"][0]["event_mission_check"]["S"].ToString().Split(splitter);
        string[] splitter_event_mission_target = json["rows"][0]["event_monster_target"]["S"].ToString().Split(splitter);
        string[] splitter_event_mission_reward = json["rows"][0]["event_monster_reward"]["S"].ToString().Split(splitter);
        string[] splitter_event_mission_package_check;
        string[] splitter_event_mission_package_limit;

        // 예전 데이터인지 체크하여 예전 데이터면 업데이트된 데이터로 변경
        if (splitter_event_mission_value.Length != 12)
        {
            splitter_event_mission_value = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        }

        if (splitter_event_mission_check.Length != 12)
        {
            splitter_event_mission_check = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        }

        if (splitter_event_mission_target.Length != 12)
        {
            splitter_event_mission_target = new string[] { "11", "1", "30", "10000", "10", "30", "1", "10", "10", "10", "20", "1" };
        }

        if (splitter_event_mission_reward.Length != 12)
        {
            splitter_event_mission_reward = new string[] { "100", "50", "50", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        }

        user_event_mission_info.inDate = json["rows"][0]["inDate"]["S"].ToString();

        user_event_mission_info.event_mission_value.Clear();
        user_event_mission_info.event_mission_check.Clear();
        user_event_mission_info.event_mission_target.Clear();
        user_event_mission_info.event_mission_reward.Clear();
        user_event_mission_info.event_mission_package_check.Clear();
        user_event_mission_info.event_mission_package_limit.Clear();

        for (int i = 0; i < 12; i++)
        {
            user_event_mission_info.event_mission_value.Add(int.Parse(splitter_event_mission_value[i]));
            user_event_mission_info.event_mission_check.Add(int.Parse(splitter_event_mission_check[i]));
            user_event_mission_info.event_mission_target.Add(int.Parse(splitter_event_mission_target[i]));
            user_event_mission_info.event_mission_reward.Add(int.Parse(splitter_event_mission_reward[i]));
        }

        // 패키지 칼럼 부분이 있는지 체크
        IDictionary check_mission_data_is_null = json[1][0] as IDictionary;
        if (check_mission_data_is_null.Contains("event_mission_package_check")) // 실제로 구매한 개수
        {
            splitter_event_mission_package_check = json["rows"][0]["event_mission_package_check"]["S"].ToString().Split(splitter);
            for (int i = 0; i < 5; i++)
            {
                user_event_mission_info.event_mission_package_check.Add(int.Parse(splitter_event_mission_package_check[i]));
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                user_event_mission_info.event_mission_package_check.Add(0);
            }
        }

        if (check_mission_data_is_null.Contains("event_mission_package_limit")) // 패키지 구매 제한 횟수
        {
            splitter_event_mission_package_limit = json["rows"][0]["event_mission_package_limit"]["S"].ToString().Split(splitter);
            for (int i = 0; i < 5; i++)
            {
                user_event_mission_info.event_mission_package_limit.Add(int.Parse(splitter_event_mission_package_limit[i]));
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                user_event_mission_info.event_mission_package_limit.Add(5);
            }
        }
    }
    //1주년 
    public void DB_Insert_user_event_1st_anniversary_info(JsonData json)
    {
        Debug.LogWarning("--------------------DB_Insert_user_event_1st_anniversary_info-------------------------");
        char[] splitter = { ',' };

        string[] splitter_event_1st_anniversary_value = json["rows"][0]["event_1st_anniversary_value"]["S"].ToString().Split(splitter);
        string[] splitter_event_1st_anniversary_check = json["rows"][0]["event_1st_anniversary_check"]["S"].ToString().Split(splitter);
        string[] splitter_event_1st_anniversary_bingo_check = json["rows"][0]["event_1st_anniversary_bingo_check"]["S"].ToString().Split(splitter);
        //string[] splitter_event_1st_anniversary_target = json["rows"][0]["event_1st_anniversary_target"]["S"].ToString().Split(splitter);
        //string[] splitter_event_1st_anniversary_reward = json["rows"][0]["event_1st_anniversary_reward"]["S"].ToString().Split(splitter);
        //string[] splitter_event_1st_anniversary_bingo_reward = json["rows"][0]["event_1st_anniversary_bingo_reward"]["S"].ToString().Split(splitter);
        //설날 각종 이벤트 보상 및 데이터 오류(이전 데이터에 덮어씌우기)
        string[] splitter_event_1st_anniversary_bingo_reward = { "50", "50", "50", "50", "50", "50", "50", "50", "100" };
        string[] splitter_event_1st_anniversary_reward = { "1", "1", "1", "1", "1", "10", "10", "1", "1" };
        string[] splitter_event_1st_anniversary_target = { "18", "50", "100", "18", "1000", "18", "150000", "5", "7" };

        string[] splitter_anniversary_attendance_reward = json["rows"][0]["anniversary_attendance_reward"]["S"].ToString().Split(splitter);
        user_event_1st_anniversary_info.anniversary_attendance_check = int.Parse(json["rows"][0]["anniversary_attendance_check"]["S"].ToString());
        user_event_1st_anniversary_info.anniversary_attendance_count = int.Parse(json["rows"][0]["anniversary_attendance_count"]["S"].ToString());
        user_event_1st_anniversary_info.anniversary_attendance_time = json["rows"][0]["anniversary_attendance_time"]["S"].ToString();

        user_event_1st_anniversary_info.inDate = json["rows"][0]["inDate"]["S"].ToString();

        user_event_1st_anniversary_info.event_1st_anniversary_value.Clear();
        user_event_1st_anniversary_info.event_1st_anniversary_check.Clear();
        user_event_1st_anniversary_info.event_1st_anniversary_bingo_check.Clear();
        user_event_1st_anniversary_info.event_1st_anniversary_target.Clear();
        user_event_1st_anniversary_info.event_1st_anniversary_reward.Clear();
        user_event_1st_anniversary_info.event_1st_anniversary_bingo_reward.Clear();

        user_event_1st_anniversary_info.anniversary_attendance_reward.Clear();

        for (int i = 0; i < 9; i++)
        {
            user_event_1st_anniversary_info.event_1st_anniversary_value.Add(int.Parse(splitter_event_1st_anniversary_value[i]));
            user_event_1st_anniversary_info.event_1st_anniversary_check.Add(int.Parse(splitter_event_1st_anniversary_check[i]));
            user_event_1st_anniversary_info.event_1st_anniversary_bingo_check.Add(int.Parse(splitter_event_1st_anniversary_bingo_check[i]));
            user_event_1st_anniversary_info.event_1st_anniversary_target.Add(int.Parse(splitter_event_1st_anniversary_target[i]));
            user_event_1st_anniversary_info.event_1st_anniversary_reward.Add(int.Parse(splitter_event_1st_anniversary_reward[i]));
            user_event_1st_anniversary_info.event_1st_anniversary_bingo_reward.Add(int.Parse(splitter_event_1st_anniversary_bingo_reward[i]));
        }

        for (int i = 0; i < splitter_anniversary_attendance_reward.Length; i++) // 14일로 변경
        {
            user_event_1st_anniversary_info.anniversary_attendance_reward.Add(int.Parse(splitter_anniversary_attendance_reward[i]));
        }

        //설 이벤트 이전 데이터인지 체크
        while (user_event_1st_anniversary_info.anniversary_attendance_reward.Count < 14)
        {
            user_event_1st_anniversary_info.anniversary_attendance_reward.Add(0);
        }

    }
    public void DB_Insert_user_year_info(JsonData json)
    {
        char[] splitter = { ',' };

        string[] splitter_attendance_reward = json[0][0]["year_attendance_reward"]["S"].ToString().Split(splitter);

        string[] splitter_package_check = json[0][0]["year_package_check"]["S"].ToString().Split(splitter);


        user_year_info.inDate = json[0][0]["inDate"]["S"].ToString();
        user_year_info.year_attendance_check = int.Parse(json[0][0]["year_attendance_check"]["S"].ToString());
        user_year_info.year_attendance_count = int.Parse(json[0][0]["year_attendance_count"]["S"].ToString());
        user_year_info.year_attendance_time = json[0][0]["year_attendance_time"]["S"].ToString();

        user_year_info.year_attendance_reward.Clear();
        for (int i = 0; i < 7; i++)
        {
            user_year_info.year_attendance_reward.Add(int.Parse(splitter_attendance_reward[i]));
        }


        user_year_info.year_package_check.Clear();
        for (int i = 0; i < 4; i++)
        {
            user_year_info.year_package_check.Add(int.Parse(splitter_package_check[i]));
        }
    }
    public void DB_Insert_user_newbie_attendance_info(JsonData json)
    {
        char[] splitter = { ',' };
        bool newbie_check = false;
        string[] splitter_newbie_attendance_reward = json[1][0]["newbie_attendance_reward"]["S"].ToString().Split(splitter);
        string[] splitter_newbie_attendance_puppy_reward;
        string[] splitter_newbie_attendance_pet_reward;

        user_newbie_attendance_info.inDate = json[1][0]["inDate"]["S"].ToString();
        user_newbie_attendance_info.newbie_attendance_check = int.Parse(json[1][0]["newbie_attendance_check"]["S"].ToString());
        user_newbie_attendance_info.newbie_attendance_count = int.Parse(json[1][0]["newbie_attendance_count"]["S"].ToString());
        user_newbie_attendance_info.newbie_attendance_time = json[1][0]["newbie_attendance_time"]["S"].ToString();
        user_newbie_attendance_info.newbie_attendance_reward.Clear();
        for (int i = 0; i < 7; i++)
        {
            user_newbie_attendance_info.newbie_attendance_reward.Add(int.Parse(splitter_newbie_attendance_reward[i]));
            if (splitter_newbie_attendance_reward[i] == "0")
                newbie_check = true;
        }

        // 테이블 칼럼 체크
        IDictionary check_newbie_data_is_null = json[1][0] as IDictionary;
        if (check_newbie_data_is_null.Contains("newbie_attendance_puppy_reward_check")) // 해당 칼럼이 있을 때
        {
            splitter_newbie_attendance_puppy_reward = json[1][0]["newbie_attendance_puppy_reward_check"]["S"].ToString().Split(splitter);
            for (int i = 0; i < 2; i++)
            {
                user_newbie_attendance_info.newbie_attendance_puppy_reward.Add(int.Parse(splitter_newbie_attendance_puppy_reward[i]));
                if (splitter_newbie_attendance_puppy_reward[i] == "0")
                    newbie_check = true;
            }
        }
        else // 없을 때
        {
            // 기존 유저가 일반 보상을 수령한 것
            if(user_newbie_attendance_info.newbie_attendance_reward[0] == 1) // 1일차 수령여부
            {
                user_newbie_attendance_info.newbie_attendance_puppy_reward.Add(1);
            }
            else
            {
                user_newbie_attendance_info.newbie_attendance_puppy_reward.Add(0);
            }
            if (user_newbie_attendance_info.newbie_attendance_reward[3] == 1) // 4일차 수령여부
            {
                user_newbie_attendance_info.newbie_attendance_puppy_reward.Add(1);
            }
            else
            {
                user_newbie_attendance_info.newbie_attendance_puppy_reward.Add(0);
            }
            /*
            for (int i = 0; i < 3; i++)
            {
                if (user_newbie_attendance_info.newbie_attendance_reward[6] == 1) // 7일차까지 받았을 때 1,1,1
                {
                    user_newbie_attendance_info.newbie_attendance_puppy_reward.Add(1);
                    newbie_check = false;
                }
                else if (user_newbie_attendance_info.newbie_attendance_reward[3] == 1) // 4일차까지 받았을 때 1,1,0
                {
                    if (i == 2)
                    {
                        user_newbie_attendance_info.newbie_attendance_puppy_reward.Add(0);
                        newbie_check = true;
                    }
                    else
                    {
                        user_newbie_attendance_info.newbie_attendance_puppy_reward.Add(1);
                    }
                }
                else if (user_newbie_attendance_info.newbie_attendance_reward[0] == 1) // 1일차까지 받았을 때 1,0,0
                {
                    if (i == 0)
                    {
                        user_newbie_attendance_info.newbie_attendance_puppy_reward.Add(1);
                    }
                    else
                    {
                        user_newbie_attendance_info.newbie_attendance_puppy_reward.Add(0);
                        newbie_check = true;
                    }
                }
                else if (user_newbie_attendance_info.newbie_attendance_reward[0] == 0) // 아무보상도 안받았을 때 0,0,0
                {
                    user_newbie_attendance_info.newbie_attendance_puppy_reward.Add(0);
                    newbie_check = true;
                }
            }*/
        }

        if (check_newbie_data_is_null.Contains("newbie_attendance_pet_reward_check")) // 해당 칼럼이 있을 때
        {
            splitter_newbie_attendance_pet_reward = json[1][0]["newbie_attendance_pet_reward_check"]["S"].ToString().Split(splitter);
            for (int i = 0; i < 2; i++)
            {
                user_newbie_attendance_info.newbie_attendance_pet_reward.Add(int.Parse(splitter_newbie_attendance_pet_reward[i]));
                if (splitter_newbie_attendance_pet_reward[i] == "0")
                    newbie_check = true;
            }
        }
        else // 없을 때
        {
            if (user_newbie_attendance_info.newbie_attendance_reward[0] == 1) // 1일차 보상 수령 여부
            {
                user_newbie_attendance_info.newbie_attendance_pet_reward.Add(1);
            }
            else
            {
                user_newbie_attendance_info.newbie_attendance_pet_reward.Add(0);
            }
            if (user_newbie_attendance_info.newbie_attendance_reward[3] == 1) // 4일차 보상 수령 여부
            {
                user_newbie_attendance_info.newbie_attendance_pet_reward.Add(1);
            }
            else
            {
                user_newbie_attendance_info.newbie_attendance_pet_reward.Add(0);
            }

            // 신규 유저 출석 이벤트 보상 다 받았는지 체크해서 버튼 활성화 유무 지정
            for (int i = 0; i < 7; i++) 
            {
                if(user_newbie_attendance_info.newbie_attendance_reward[i] == 0)
                {
                    newbie_check = true;
                }
                else
                {
                    newbie_check = false;
                }
            }

            /*
            for (int i = 0; i < 3; i++)
            {
                if (user_newbie_attendance_info.newbie_attendance_reward[6] == 1) // 7일차까지 다 받았을 때 1,1,1
                {
                    user_newbie_attendance_info.newbie_attendance_pet_reward.Add(1);
                    newbie_check = false;
                }
                else if (user_newbie_attendance_info.newbie_attendance_reward[3] == 1) // 4일차까지 받았을 때 1,1,0
                {
                    if (i == 2)
                    {
                        user_newbie_attendance_info.newbie_attendance_pet_reward.Add(0);
                        newbie_check = true;
                    }
                    else
                    {
                        user_newbie_attendance_info.newbie_attendance_pet_reward.Add(1);
                    }
                }
                else if (user_newbie_attendance_info.newbie_attendance_reward[0] == 1) // 1일차까지 받았을 때 1,0,0
                {
                    if (i == 0)
                    {
                        user_newbie_attendance_info.newbie_attendance_pet_reward.Add(1);
                    }
                    else
                    {
                        user_newbie_attendance_info.newbie_attendance_pet_reward.Add(0);
                        newbie_check = true;
                    }
                }
                else if (user_newbie_attendance_info.newbie_attendance_reward[0] == 0) // 아무 보상도 안받았을 때 0,0,0
                {
                    user_newbie_attendance_info.newbie_attendance_pet_reward.Add(0);
                    newbie_check = true;
                }
            }
            */
            DB_Update_user_newbie_attendance_info();
        }

        if (newbie_check == false)
            ServerManager.Instance.newbie_event_time = false;
    }

    public void DB_Insert_user_attendance_package_info(JsonData json)
    {
        char[] splitter = { ',' };

        user_attendance_package.inDate = json[1][0]["inDate"]["S"].ToString();

        string[] splitter_nomal_attendance_package_reward = json[1][0]["nomal_attendance_package_reward"]["S"].ToString().Split(splitter);
        string[] splitter_premium_attendance_package_reward = json[1][0]["premium_attendance_package_reward"]["S"].ToString().Split(splitter);

        user_attendance_package.nomal_attendance_package_reward_count = int.Parse(json[1][0]["nomal_attendance_package_reward_count"]["S"].ToString());
        user_attendance_package.nomal_attendance_package_time = json[1][0]["nomal_attendance_package_time"]["S"].ToString();
        user_attendance_package.nomal_attendance_package_count = int.Parse(json[1][0]["nomal_attendance_package_count"]["S"].ToString());
        user_attendance_package.nomal_attendance_package_check = int.Parse(json[1][0]["nomal_attendance_package_check"]["S"].ToString());
        user_attendance_package.nomal_attendance_package_reward.Clear();
        for (int i = 0; i < 30; i++)
        {
            user_attendance_package.nomal_attendance_package_reward.Add(int.Parse(splitter_nomal_attendance_package_reward[i]));
        }

        user_attendance_package.premium_attendance_package_reward_count = int.Parse(json[1][0]["premium_attendance_package_reward_count"]["S"].ToString());
        user_attendance_package.premium_attendance_package_time = json[1][0]["premium_attendance_package_time"]["S"].ToString();
        user_attendance_package.premium_attendance_package_count = int.Parse(json[1][0]["premium_attendance_package_count"]["S"].ToString());
        user_attendance_package.premium_attendance_package_check = int.Parse(json[1][0]["premium_attendance_package_check"]["S"].ToString());
        user_attendance_package.premium_attendance_package_reward.Clear();
        for (int i = 0; i < 30; i++)
        {
            user_attendance_package.premium_attendance_package_reward.Add(int.Parse(splitter_premium_attendance_package_reward[i]));
        }
    }

    public void DB_Update_user_boss_info()
    {
        Debug.Log("-DB_Update_user_boss_info-");
        Param param = new Param();
        param.Add("boss_index", Update_list_converter(user_boss_info.boss_index));
        param.Add("boss_life", Update_list_long_converter(user_boss_info.boss_life));
        param.Add("boss_end_time", Update_list_string_converter(user_boss_info.boss_end_time));
        param.Add("boss_ticket", user_boss_info.ticket_count.GetDecrypted());
        param.Add("boss_ticket_time", user_boss_info.ticket_time);

        Backend.GameData.Update("user_new_boss_info", user_boss_info.inDate, param, (callback) =>
        {
            if (callback.GetStatusCode() == "204")
            {
                //Debug.Log("------------------------DB_Update_user_boss_info success");
            }
            else
            {
                Debug.Log("boss errrrrrrrrrrrrrrrrrrrrrrrrr" + callback.GetStatusCode());
                ServerManager.Instance.error_type = 18;
                ServerManager.Instance.server_loading = false;
                ServerManager.Instance.Server_connect_check();
                ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
            }

        });
    }
    public void DB_Update_user_rune_info()
    {
        Debug.Log("-DB_Update_user_rune_info-");
        Param param = new Param();
        param.Add("rune_level", Update_list_converter(user_rune_info.rune_level));
        param.Add("rune_stock", Update_list_converter(user_rune_info.rune_stock));
        param.Add("rune_stage", user_rune_info.rune_stage.GetDecrypted().ToString());
        param.Add("rune_ad_time", user_rune_info.rune_ad_time);
        param.Add("rune_ticket", user_rune_info.rune_ticket.GetDecrypted().ToString());
        param.Add("rune_time", user_rune_info.rune_time);

        Backend.GameData.Update("user_rune_info", user_rune_info.inDate, param, (callback) =>
        {
            if (callback.GetStatusCode() == "204")
            {
                //Debug.Log("------------------------DB_Update_user_rune_info success");
            }
            else
            {
                ServerManager.Instance.error_type = 24;
                ServerManager.Instance.server_loading = false;
                ServerManager.Instance.Server_connect_check();
                ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
            }

        });
    }
    public void DB_Update_user_pet_info()
    {
        Debug.Log("DB_Insert_user_pet_info");
        char[] splitter = { ',' };
        char[] splitter1 = { '/' };

        Param param = new Param();
        param.Add("pet_active", user_pet_info.pet_active.ToString());
        param.Add("select_pet_index", user_pet_info.select_pet_index.ToString());
        param.Add("select_pet_type", user_pet_info.select_pet_type.ToString());
        param.Add("select_pet_isAwakening", user_pet_info.select_pet_isAwakening.ToString());
        param.Add("user_pet", Update_list_converter(user_pet_info.user_pet));
        param.Add("pet_awakening_value", Update_list_converter(user_pet_info.pet_awakening_value));

        Backend.GameData.Update("user_pet_info", user_pet_info.inDate, param, (callback) =>
        {
            if (callback.GetStatusCode() == "204")
            {
                //Debug.Log("------------------------DB_Update_user_rune_info success");
            }
            else
            {
                ServerManager.Instance.error_type = 34;
                ServerManager.Instance.server_loading = false;
                ServerManager.Instance.Server_connect_check();
                ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
            }

        });
    }

    public void DB_Update_user_newbie_attendance_info()
    {
        Debug.Log("-DB_Update_user_newbie_attendance_info-");
        Param param = new Param();
        param.Add("newbie_attendance_check", user_newbie_attendance_info.newbie_attendance_check.GetDecrypted().ToString());
        param.Add("newbie_attendance_time", user_newbie_attendance_info.newbie_attendance_time.ToString());
        param.Add("newbie_attendance_count", user_newbie_attendance_info.newbie_attendance_count.GetDecrypted().ToString());
        param.Add("newbie_attendance_reward", Update_list_converter(user_newbie_attendance_info.newbie_attendance_reward));
        param.Add("newbie_attendance_puppy_reward_check", Update_list_converter(user_newbie_attendance_info.newbie_attendance_puppy_reward));
        param.Add("newbie_attendance_pet_reward_check", Update_list_converter(user_newbie_attendance_info.newbie_attendance_pet_reward));

        Backend.GameData.Update("user_newbie_attendance_info", user_newbie_attendance_info.inDate, param, (callback) =>
        {

            if (callback.GetStatusCode() == "204")
            {

            }
            else
            {
                ServerManager.Instance.error_type = 26;
                ServerManager.Instance.server_loading = false;
                ServerManager.Instance.Server_connect_check();
                ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
            }

        });
    }


    public void DB_Update_user_attendance_package_info()
    {
        Debug.Log("-DB_Update_user_attendance_package_info-");
        Param param = new Param();
        param.Add("nomal_attendance_package_check", user_attendance_package.nomal_attendance_package_check.GetDecrypted().ToString());
        param.Add("nomal_attendance_package_time", user_attendance_package.nomal_attendance_package_time.ToString());
        param.Add("nomal_attendance_package_count", user_attendance_package.nomal_attendance_package_count.GetDecrypted().ToString());
        param.Add("nomal_attendance_package_reward_count", user_attendance_package.nomal_attendance_package_reward_count.GetDecrypted().ToString());
        param.Add("nomal_attendance_package_reward", Update_list_converter(user_attendance_package.nomal_attendance_package_reward));

        param.Add("premium_attendance_package_check", user_attendance_package.premium_attendance_package_check.GetDecrypted().ToString());
        param.Add("premium_attendance_package_time", user_attendance_package.premium_attendance_package_time.ToString());
        param.Add("premium_attendance_package_count", user_attendance_package.premium_attendance_package_count.GetDecrypted().ToString());
        param.Add("premium_attendance_package_reward_count", user_attendance_package.premium_attendance_package_reward_count.GetDecrypted().ToString());
        param.Add("premium_attendance_package_reward", Update_list_converter(user_attendance_package.premium_attendance_package_reward));

        Backend.GameData.Update(ServerManager.Instance.user_attendance_package_table, user_attendance_package.inDate, param, (callback) =>
        {

            if (callback.GetStatusCode() == "204")
            {

            }
            else
            {
                ServerManager.Instance.error_type = 39;
                ServerManager.Instance.server_loading = false;
                ServerManager.Instance.Server_connect_check();
                ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
            }

        });
    }

    public void DB_Update_user_event_info()
    {
        Debug.Log("-------------------DB_Update_user_year_info-------------------");
        Param param = new Param();

        param.Add("event_monster_point", user_event_info.event_monster_point.ToString());

        param.Add("event_monster_total", user_event_info.event_monster_total.ToString());

        param.Add("event_monster_check", Update_list_converter(user_event_info.event_monster_check));  // 포인트 교환 횟수

        param.Add("event_package_check", Update_list_converter(user_event_info.event_package_check)); // 패키지 구매 채크

        param.Add("event_package_limit", Update_list_converter(user_event_info.event_package_limit));

        Backend.GameData.Update("user_event_point", user_event_info.inDate, param, (callback) =>
        {

            if (callback.GetStatusCode() == "204")
            {

            }
            else
            {
                ServerManager.Instance.error_type = 35;
                ServerManager.Instance.server_loading = false;
                ServerManager.Instance.Server_connect_check();
                ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
            }

        });
    }

    public void DB_Update_user_event_pay_info()
    {
        Debug.Log("--------------------DB_Update_user_event_pay_info-------------------------");

        Param param = new Param();

        param.Add("user_pay", user_event_pay_info.pay_point.GetDecrypted().ToString());

        param.Add("event_reset_check", user_event_pay_info.event_reset_check.ToString());

        param.Add("user_pay_reward_check", Update_list_converter(user_event_pay_info.pay_reward_check));  // 포인트 교환 횟수

        Backend.GameData.Update("user_event_pay", user_event_pay_info.inDate, param, (callback) =>
        {

            if (callback.GetStatusCode() == "204")
            {
            }
            else
            {
                ServerManager.Instance.error_type = 41;
                ServerManager.Instance.server_loading = false;
                ServerManager.Instance.Server_connect_check();
                ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
            }

        });
    }
    public void DB_Update_user_event_playTime_info()
    {
        Debug.Log("--------------------DB_Update_user_event_playTime_info-------------------------");

        Param playTimeParam = new Param();

        playTimeParam.Add("user_playTime", user_event_playTime_info.playTime.GetDecrypted().ToString());

        playTimeParam.Add("user_playTime_reward_check", Update_list_string_converter(user_event_playTime_info.playTime_reward_check));  // 포인트 교환 횟수

        Backend.GameData.Update("user_event_playTime", user_event_playTime_info.inDate, playTimeParam, (callback) =>
        {

            if (callback.GetStatusCode() == "204")
            {
                retry = 0;

            }
            else
            {
                retry++;

                if (retry <= 4)
                {
                    DB_Update_user_event_playTime_info();
                }
                else if (retry > 4)
                {
                    ServerManager.Instance.error_type = 42;
                    ServerManager.Instance.server_loading = false;
                    ServerManager.Instance.Server_connect_check();
                    ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString() + user_event_playTime_info.inDate);
                }
                else
                {
                    Invoke("DB_Update_user_event_playTime_info", 15f);
                }
            }

        });
    }

    public void DB_Update_user_last_connected_time()
    {
        Debug.LogWarning("-------------------------------DB_Update_user_last_connected_time------------------------------");
        Param userInfoParam = new Param();

        userInfoParam.Add("last_connected_time", user_info.last_connected_time);
        userInfoParam.Add("user_item_time", Update_list_string_converter(user_info.user_item_time));

        Backend.GameData.Update("user_info", user_info.inDate, userInfoParam, (callback) =>
        {
            if (callback.GetStatusCode() == "204")
            {

            }
            else
            {
                Debug.LogError(callback.GetMessage());
                ServerManager.Instance.Server_connect_check();
            }
        });

    }

    public void DB_Update_user_event_mission_info()
    {
        if (ServerManager.Instance.event_mission_time == true)
        {
            if (user_mission_update_time > 10)
            {
                user_mission_update_time = 0;

                Param param = new Param();

                param.Add("event_mission_value", Update_list_converter(user_event_mission_info.event_mission_value));
                param.Add("event_mission_check", Update_list_converter(user_event_mission_info.event_mission_check));
                param.Add("event_monster_reward", Update_list_converter(user_event_mission_info.event_mission_reward));
                param.Add("event_monster_target", Update_list_converter(user_event_mission_info.event_mission_target));
                param.Add("event_mission_package_check", Update_list_converter(user_event_mission_info.event_mission_package_check));
                param.Add("event_mission_package_limit", Update_list_converter(user_event_mission_info.event_mission_package_limit));

                Backend.GameData.Update("user_event_mission", user_event_mission_info.inDate, param, (callback) =>
                {
                    if (callback.GetStatusCode() == "204")
                    {
                    }
                    else
                    {
                        ServerManager.Instance.error_type = 43;
                        ServerManager.Instance.server_loading = false;
                        ServerManager.Instance.Server_connect_check();
                        ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
                    }
                });
            }
        }
    }

    public void DB_Update_user_event_1st_anniversary_info()
    {
        if (ServerManager.Instance.event_1st_anniversary_time == true)
        {
            if (user_1st_anniversary_update_time >= 20)
            {
                user_1st_anniversary_update_time = 0;

                Param param = new Param();

                param.Add("event_1st_anniversary_value", Update_list_converter(user_event_1st_anniversary_info.event_1st_anniversary_value));
                param.Add("event_1st_anniversary_check", Update_list_converter(user_event_1st_anniversary_info.event_1st_anniversary_check));
                param.Add("event_1st_anniversary_bingo_check", Update_list_converter(user_event_1st_anniversary_info.event_1st_anniversary_bingo_check));

                param.Add("anniversary_attendance_check", user_event_1st_anniversary_info.anniversary_attendance_check.GetDecrypted().ToString());
                param.Add("anniversary_attendance_time", user_event_1st_anniversary_info.anniversary_attendance_time.ToString());
                param.Add("anniversary_attendance_count", user_event_1st_anniversary_info.anniversary_attendance_count.GetDecrypted().ToString());
                param.Add("anniversary_attendance_reward", Update_list_converter(user_event_1st_anniversary_info.anniversary_attendance_reward));

                Backend.GameData.Update("Private_table_0", user_event_1st_anniversary_info.inDate, param, (callback) =>
                {
                    if (callback.GetStatusCode() == "204")
                    {
                        //Debug.LogWarning("1주년 이벤트 db->server업데이트성공");
                    }
                    else
                    {
                        // Debug.LogWarning("1주년 이벤트 db->server업데이트 에러");
                        ServerManager.Instance.error_type = 43;
                        ServerManager.Instance.server_loading = false;
                        ServerManager.Instance.Server_connect_check();
                        ServerManager.Instance.send_log_server_error(ServerManager.Instance.error_type, callback.GetStatusCode().ToString(), callback.GetMessage().ToString());
                    }
                });
            }
        }
    }

    public void DB_Insert_collection_info(JsonData json) // 공용 콜렉션 데이터 DB 인서트
    {
        for (int i = 0; i < json[1].Count; i++)
        {
            Collection_info collection_temp = new Collection_info();
            collection_temp.collection_index = int.Parse(json[1][i]["index"]["S"].ToString());
            collection_temp.collection_name = json[1][i]["name"]["S"].ToString();
            collection_temp.unit_1_index = json[1][i]["unit_1"]["S"].ToString();
            collection_temp.unit_2_index = json[1][i]["unit_2"]["S"].ToString();
            collection_temp.unit_3_index = json[1][i]["unit_3"]["S"].ToString();
            collection_temp.unit_4_index = json[1][i]["unit_4"]["S"].ToString();
            collection_temp.unit_5_index = json[1][i]["unit_5"]["S"].ToString();
            collection_temp.ability_type = int.Parse(json[1][i]["ability_index"]["S"].ToString());
            collection_temp.ability_value = int.Parse(json[1][i]["ability_value"]["S"].ToString());
            list_collection_info.Add(collection_temp);
        }

        list_collection_info.Sort(delegate (Collection_info a, Collection_info b) { return a.collection_index.CompareTo(b.collection_index); });
        ServerManager.Instance.get_collection_table_check = true;
    }
    public void DB_Insert_pet_collection_info(JsonData json) // 공용 콜렉션 데이터 DB 인서트
    {
        for (int i = 0; i < pet_collection_count; i++)
        {
            Pet_collection_info collection_temp = new Pet_collection_info();
            collection_temp.collection_index = int.Parse(json[1][i]["index"]["S"].ToString());

            collection_temp.pet_1 = json[1][i]["pet_1"]["S"].ToString();
            collection_temp.pet_2 = json[1][i]["pet_2"]["S"].ToString();
            collection_temp.pet_3 = json[1][i]["pet_3"]["S"].ToString();
            collection_temp.pet_4 = json[1][i]["pet_4"]["S"].ToString();
            collection_temp.pet_grade = json[1][i]["pet_grade"]["S"].ToString();
            collection_temp.ability_type = int.Parse(json[1][i]["ability_index"]["S"].ToString());
            collection_temp.ability_value = int.Parse(json[1][i]["ability_value"]["S"].ToString());
            list_pet_collection_info.Add(collection_temp);
        }
        list_pet_collection_info.Sort(delegate (Pet_collection_info a, Pet_collection_info b) { return a.collection_index.CompareTo(b.collection_index); });
        ServerManager.Instance.get_pet_collection_table_check = true;
    }
    public void DB_Insert_upgrade_info(JsonData json) // 공용 업그레이드 데이터 DB 인서트
    {
        for (int i = 0; i < json[1].Count; i++)
        {
            Upgrade_info upgrade_temp = new Upgrade_info();
            upgrade_temp.upgrade_index = int.Parse(json[1][i]["index"]["S"].ToString());
            upgrade_temp.upgrade_name = json[1][i]["name"]["S"].ToString();
            upgrade_temp.upgrade_info = json[1][i]["info"]["S"].ToString();
            upgrade_temp.gold_price = int.Parse(json[1][i]["gold_price"]["S"].ToString());
            upgrade_temp.diamond_price = int.Parse(json[1][i]["dia_price"]["S"].ToString());
            upgrade_temp.again_price = int.Parse(json[1][i]["again_price"]["S"].ToString());

            list_upgrade_info.Add(upgrade_temp);
        }
        ServerManager.Instance.get_upgrade_table_check = true;
    }
    public void DB_Insert_attendance_info(JsonData json) // 공용 출석체크 데이터 DB 인서트
    {
        char[] splitter = { ',' };

        for (int i = 0; i < json["rows"].Count; i++)
        {
            // 출석 보상 차트로 개선
            attendance_info.attendance_reward_type.Add(int.Parse(json["rows"][i]["attendance_reward_type"]["S"].ToString()));
            attendance_info.attendance_reward_value.Add(int.Parse(json["rows"][i]["attandance_reward_value"]["S"].ToString()));
            if (int.Parse(json["rows"][i]["attandance_special_reward_value"]["S"].ToString()) != 0)
            {
                attendance_info.attendance_special_reward_type.Add(int.Parse(json["rows"][i]["attandance_special_reward_type"]["S"].ToString()));
                attendance_info.attendance_special_reward_value.Add(int.Parse(json["rows"][i]["attandance_special_reward_value"]["S"].ToString()));
            }
            
            /*
            string[] splitter_attendance_reward_type = json[1][i]["attendance_reward_type"]["S"].ToString().Split(splitter);
            string[] splitter_attendance_reward_value = json[1][i]["attendance_reward_value"]["S"].ToString().Split(splitter);

            string[] splitter_attendance_special_reward_type = json[1][i]["attendance_special_reward_type"]["S"].ToString().Split(splitter);
            string[] splitter_attendance_special_reward_value = json[1][i]["attendance_special_reward_value"]["S"].ToString().Split(splitter);

            for (int s = 0; s < splitter_attendance_reward_type.Length; s++)
            {
                attendance_info.attendance_reward_type.Add(int.Parse(splitter_attendance_reward_type[s].ToString()));
                attendance_info.attendance_reward_value.Add(int.Parse(splitter_attendance_reward_value[s].ToString()));

            }
            for (int s = 0; s < splitter_attendance_special_reward_type.Length; s++)
            {
                attendance_info.attendance_special_reward_type.Add(int.Parse(splitter_attendance_special_reward_type[s].ToString()));
                attendance_info.attendance_special_reward_value.Add(int.Parse(splitter_attendance_special_reward_value[s].ToString()));
            }*/
            
        }
        ServerManager.Instance.get_attendance_table_check = true;
    }
    public string Language_converter(int index)
    {
        //Debug.Log("번역 개수" +List_language[language_index].Count);
        return List_language[language_index][index];
    }
    public int obscureint_converter(ObscuredInt value)
    {
        return value;
    }
    public long obscurelong_converter(ObscuredLong value)
    {
        return value;
    }
    public void Attendance_reset()
    {
        user_info.attendance_count = 1;
        for (int i = 0; i < user_info.attendance_reward.Count; i++)
        {
            user_info.attendance_reward[i] = 0;
        }
        for (int i = 0; i < user_info.attendance_special_reward.Count; i++)
        {
            user_info.attendance_special_reward[i] = 0;
        }
    }
    public void random_obscure_unitinfo()
    {
        for (int i = 0; i < list_unit_info.Count; i++)
        {
            list_unit_info[i].unit_index.RandomizeCryptoKey();
            list_unit_info[i].grade.RandomizeCryptoKey();
            list_unit_info[i].attack_damage.RandomizeCryptoKey();
            list_unit_info[i].attack_speed.RandomizeCryptoKey();
            list_unit_info[i].life.RandomizeCryptoKey();
            list_unit_info[i].critical_damage.RandomizeCryptoKey();
            list_unit_info[i].critical_percent.RandomizeCryptoKey();
            list_unit_info[i].ability_type.RandomizeCryptoKey();
            list_unit_info[i].ability_value.RandomizeCryptoKey();
        }
    }
    public void random_obscure_bossinfo()
    {
        for (int i = 0; i < list_boss_info.Count; i++)
        {
            list_boss_info[i].boss_grade.RandomizeCryptoKey();
            list_boss_info[i].boss_life.RandomizeCryptoKey();
            list_boss_info[i].boss_damage.RandomizeCryptoKey();
            list_boss_info[i].boss_attack_speed.RandomizeCryptoKey();
        }
        for (int i = 0; i < user_boss_info.boss_index.Count; i++)
        {
            user_boss_info.boss_index[i].RandomizeCryptoKey();
        }
        for (int i = 0; i < user_boss_info.boss_life.Count; i++)
        {
            user_boss_info.boss_life[i].RandomizeCryptoKey();
        }
        for (int i = 0; i < user_boss_info.boss_end_time.Count; i++)
        {
            user_boss_info.boss_end_time[i].RandomizeCryptoKey();
        }
        //user_boss_info.ticket_time.RandomizeCryptoKey();
        user_boss_info.ticket_count.RandomizeCryptoKey();
    }

    public void random_obscure_treasure_info()
    {
        for (int i = 0; i < tresure_box_price.Count; i++)
        {
            tresure_box_price[i].RandomizeCryptoKey();
        }
    }
    public void random_obscure_achievement_info()
    {
        for (int i = 0; i < list_achievement_info.Count; i++)
        {
            for (int ii = 0; ii < list_achievement_info[i].list_reward.Count; ii++)
            {
                list_achievement_info[i].list_reward[ii].RandomizeCryptoKey();
            }
            for (int ii = 0; ii < list_achievement_info[i].list_target.Count; ii++)
            {
                list_achievement_info[i].list_target[ii].RandomizeCryptoKey();
            }
        }
    }
    public void random_obscure_quest_info()
    {
        for (int i = 0; i < list_quest_info.Count; i++)
        {
            list_quest_info[i].quest_reward.RandomizeCryptoKey();
            list_quest_info[i].quest_target.RandomizeCryptoKey();
        }
    }
    public void random_obscure_rune_info()
    {
        user_rune_info.rune_stage.RandomizeCryptoKey();
        user_rune_info.rune_ticket.RandomizeCryptoKey();

        for (int i = 0; i < user_rune_info.rune_level.Count; i++)
        {
            user_rune_info.rune_level[i].RandomizeCryptoKey();
            user_rune_info.rune_stock[i].RandomizeCryptoKey();
        }
    }
    public void random_obscure_userinfo()
    {
        //userinfo

        user_info.pvp_point.RandomizeCryptoKey();
        user_info.gold.RandomizeCryptoKey();
        user_info.diamond.RandomizeCryptoKey();
        user_info.point.RandomizeCryptoKey();
        user_info.power.RandomizeCryptoKey();
        for (int i = 0; i < user_info.user_unit.Count; i++)
        {
            user_info.user_unit[i].RandomizeCryptoKey();
        }

        user_info.select_unit.RandomizeCryptoKey();
        user_info.user_stage.RandomizeCryptoKey();
        user_info.review.RandomizeCryptoKey();
        user_info.user_level.RandomizeCryptoKey();
        user_info.user_skill_point.RandomizeCryptoKey();
        user_info.user_experience.RandomizeCryptoKey();
        for (int i = 0; i < user_info.user_skill.Count; i++)
        {
            user_info.user_skill[i].RandomizeCryptoKey();
        }
        for (int i = 0; i < user_info.user_achievements.Count; i++)
        {
            user_info.user_achievements[i].RandomizeCryptoKey();
        }
        user_info.monster_count.RandomizeCryptoKey();
        user_info.boss_count.RandomizeCryptoKey();
        user_info.ad_count.RandomizeCryptoKey();
        user_info.total_stage.RandomizeCryptoKey();
        user_info.best_stage.RandomizeCryptoKey();
        user_info.tresure_box_count.RandomizeCryptoKey();
        user_info.total_again.RandomizeCryptoKey();
        for (int i = 0; i < user_info.user_quest_reward.Count; i++)
        {
            user_info.user_quest_reward[i].RandomizeCryptoKey();
        }
        for (int i = 0; i < user_info.user_quest_value.Count; i++)
        {
            user_info.user_quest_value[i].RandomizeCryptoKey();
        }
        user_info.attendance_check.RandomizeCryptoKey();
        user_info.attendance_count.RandomizeCryptoKey();
        for (int i = 0; i < user_info.attendance_reward.Count; i++)
        {
            user_info.attendance_reward[i].RandomizeCryptoKey();
        }
        for (int i = 0; i < user_info.attendance_special_reward.Count; i++)
        {
            user_info.attendance_special_reward[i].RandomizeCryptoKey();
        }
        user_info.attendance_level.RandomizeCryptoKey();

        user_info.upgrade_gold_attack_damage.RandomizeCryptoKey();
        user_info.upgrade_gold_attack_speed.RandomizeCryptoKey();
        user_info.upgrade_gold_critical_damage.RandomizeCryptoKey();
        user_info.upgrade_gold_critical_percent.RandomizeCryptoKey();
        user_info.upgrade_gold_life.RandomizeCryptoKey();
        user_info.upgrade_gold_enemy_life.RandomizeCryptoKey();
        user_info.upgrade_gold_again.RandomizeCryptoKey();

        user_info.upgrade_diamond_attack_damage.RandomizeCryptoKey();
        user_info.upgrade_diamond_attack_speed.RandomizeCryptoKey();
        user_info.upgrade_diamond_critical_damage.RandomizeCryptoKey();
        user_info.upgrade_diamond_critical_percent.RandomizeCryptoKey();
        user_info.upgrade_diamond_life.RandomizeCryptoKey();
        user_info.upgrade_diamond_enemy_life.RandomizeCryptoKey();
        user_info.upgrade_diamond_again.RandomizeCryptoKey();

        user_info.upgrade_point_attack_damage.RandomizeCryptoKey();
        user_info.upgrade_point_attack_speed.RandomizeCryptoKey();
        user_info.upgrade_point_critical_damage.RandomizeCryptoKey();
        user_info.upgrade_point_critical_percent.RandomizeCryptoKey();
        user_info.upgrade_point_life.RandomizeCryptoKey();
        user_info.upgrade_point_enemy_life.RandomizeCryptoKey();
        user_info.upgrade_point_again.RandomizeCryptoKey();

        user_info.free_diamond_ticket.RandomizeCryptoKey();
        user_info.point_treasure_ticket.RandomizeCryptoKey();
        user_info.package_active.RandomizeCryptoKey();

        for (int i = 0; i < user_info.user_item.Count; i++)
        {
            user_info.user_item[i].RandomizeCryptoKey();
        }
        user_info.pvp_ticket.RandomizeCryptoKey();

        for (int i = 0; i < user_info.compose_stack.Count; i++)
        {
            user_info.compose_stack[i].RandomizeCryptoKey();
        }
    }
    public void terms_date_save()
    {
        //Debug.Log("save local " + Application.persistentDataPath);
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/terms.dat");

        bf.Serialize(file, terms);
        file.Close();
    }
    public void terms_date_load()
    {
        //Debug.Log("save local " + Application.persistentDataPath);
        if (File.Exists(Application.persistentDataPath + "/terms.dat")) // 내부데이터베이스에서 로드
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/terms.dat", FileMode.Open);
            terms = (int)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            terms = 0;
        }
        terms_date_save();
    }
}