#include "s_GameScene.h"
#include "Boss/Boss.h"
#include "Player/Player.h"
#include "enemies/enemyManager.h"
#include "hitManager.h"
#include "bulletManager.h"
#include "BackGround.h"
#include "uiManager.h"
bool s_GameScene::init()
{

    Player* play = Player::getIns();
    this->addChild(play);
    play->setName("Player");

    enemyManager* em = enemyManager::getIns();
    this->addChild(em);
    
    
    BackGround* bg = BackGround::create();
    this->addChild(bg);
    bg->setAnchorPoint(Vec2(0, 0));
    bg->setZOrder(-1);
    
    hitManager* hit = hitManager::getIns();
    this->addChild(hit);
   
    bulletManager* bullet = bulletManager::getIns();
    this->addChild(bullet);

    uiManager* ui = uiManager::create();
    this->addChild(ui);

    bossTrue = false;

    this->scheduleUpdate();

    
    return true;
}

void s_GameScene::update(float dt)
{
    Time += dt;
    if (Time >= 20 && bossTrue == false)
    {
        enemyManager::getIns()->eM_DisAct();
        Boss* boss = Boss::create();
        this->addChild(boss);
        boss->setName("Boss");
        boss->setPosition(Vec2(1300,200));
        bossTrue = true;
    }
    
}
