#include "s_GameScene.h"
#include "Boss/Boss.h"
#include "Player/Player.h"
#include "enemies/enemyManager.h"
#include "hitManager.h"
#include "bulletManager.h"
#include "BackGround.h"
#include "uiManager.h"
#include "soundManager.h"

bool s_GameScene::init()
{
    Player* play = Player::getIns();
    play->reset();
    play->setParent(NULL);
    this->addChild(play);
    play->setName("Player");


    enemyManager* em = enemyManager::getIns();
    em->eM_DisAct();
    em->reset();
    em->setParent(NULL);
    this->addChild(em);
    
    
    BackGround* bg = BackGround::create();
    bg->setParent(NULL);
    this->addChild(bg);
    bg->setAnchorPoint(Vec2(0, 0));
    bg->setZOrder(-1);
    
    hitManager* hit = hitManager::getIns();
    hit->reset();
    hit->setParent(NULL);
    this->addChild(hit);
   
    bulletManager* bullet = bulletManager::getIns();
    bullet->reset();
    bullet->setParent(NULL);
    this->addChild(bullet);

    uiManager* ui = uiManager::create();
    ui->setParent(NULL);
    this->addChild(ui);

    soundManager* s = soundManager::getIns();
    s->setParent(NULL);
    this->addChild(s);
    s->bgm(0);
    s->sfx(6);

    bossTrue = false;

    this->scheduleUpdate();

    
    return true;
}

void s_GameScene::update(float dt)
{
    Time += dt;
    if (Time >= 60 && bossTrue == false)
    {
        soundManager::getIns()->bgm(1);
        enemyManager::getIns()->eM_DisAct();
        Boss* boss = Boss::create();
        this->addChild(boss);
        boss->setName("Boss");
        boss->setPosition(Vec2(1300,200));
        bossTrue = true;
    }
    
}
