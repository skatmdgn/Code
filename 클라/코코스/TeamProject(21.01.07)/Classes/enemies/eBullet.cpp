#include "eBullet.h"
#include "../bulletManager.h"

bool eBullet::init()
{
    Sprite* bul = Sprite::create("BULLET.png");
    this->addChild(bul);
    bul->setName("bul");
    bul->setColor(Color3B::RED);

    dir = Vec2(1, 0);
    speed = 500;

    this->setPosition(Vec2(9999, 999));

    eb_DisAct();

    return true;
}

void eBullet::update(float dt)
{
    this->setPosition(this->getPosition() - speed * dt * dir);
}

void eBullet::damage()
{
    this->setPositionX(-40);
}

void eBullet::scale()
{
    Sprite* spr = (Sprite*)this->getChildByName("bul");
    spr->setScale(6);
}

void eBullet::eb_Active(Vec2 v)
{
    this->setPosition(v);
    this->setPositionX(this->getPositionX() - 55);
    
    this->setVisible(true);

    this->scheduleUpdate();
}

void eBullet::eb_DisAct()
{
    Sprite* spr = (Sprite*)this->getChildByName("bul");
    spr->setScale(1);
    this->stopAllActions();
    this->unscheduleUpdate();
    this->setVisible(false);
}

Rect eBullet::getBox()
{
    Sprite* spr = (Sprite*)this->getChildByName("bul");
    Rect rt = spr->getBoundingBox();
    rt.origin += this->getPosition();

    return rt;
}
