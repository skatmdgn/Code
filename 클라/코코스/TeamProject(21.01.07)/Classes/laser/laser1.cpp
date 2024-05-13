#include "laser1.h"
#include "Player/Player.h"
#include "bulletManager.h"

bool laser1::init()
{
    Sprite* laser = Sprite::create("shot_2.png");
    this->addChild(laser);
    laser->setName("laser");
    laser1_DisAct();

    return true;
}

void laser1::update(float dt)
{
    this->setPositionX(this->getPositionX() + 800 * dt);
}

void laser1::laser1_Active()
{
    this->setPositionX(Player::getIns()->getChildByName("player")->getPositionX() + 40);
    this->setPositionY(Player::getIns()->getChildByName("player")->getPositionY());
    
    this->setVisible(true);
    
    this->scheduleUpdate();
}

void laser1::laser1_DisAct()
{
    this->setVisible(false);
    
    this->unscheduleUpdate();
}

void laser1::collision()
{
    this->setPositionX(1300);
}

Rect laser1::getBox()
{
    Sprite* l = (Sprite*)this->getChildByName("laser");
    Rect rt = l->getBoundingBox();
    rt.origin += this->getPosition();

    return rt;
}

