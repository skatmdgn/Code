#include "enemy1.h"
#include "../Player/Player.h"
#include "soundManager.h"

bool enemy1::init()
{
    Sprite* en1 = Sprite::create("enemy1.png");
    this->addChild(en1);
    en1->setName("en1");

    this->setPosition(Vec2(9999, 999));

    enemy1_DisAct();

    return true;
}

void enemy1::update(float dt)
{
    if (this->getPositionX() <= 1220 && target == false&&this->getPositionX()>=0)
    {
        dir = Player::getIns()->getChildByName("player")->getPosition() - this->getPosition();
        dir = dir.getNormalized();
        target = true;
    }

    if (target == true)
    {
        this->setPosition(this->getPosition() + dir * dt * 600);

        ang = atan2f(dir.x, dir.y);
        ang = CC_RADIANS_TO_DEGREES(ang);
        Sprite* spr = (Sprite*)this->getChildByName("en1");
        spr->setRotation(ang + 90);
    }
    else
    {
        this->setPositionX(this->getPositionX() - dt * 600);
    }
}

void enemy1::enemy1_Active()
//에너미 활성화 함수
{
    target = false;

    int ranY = rand() % 671 + 25;
    this->setPosition(Vec2(1315, ranY));

    this->setVisible(true);

    this->scheduleUpdate();
}

void enemy1::enemy1_DisAct()
//에너미 비활성화 함수
{
    Sprite* spr = (Sprite*)this->getChildByName("en1");
    spr->setRotation(0);
    this->setVisible(false);
    this->unscheduleUpdate();
}

void enemy1::damage()
//에너미 데미지 받았을 때 처리
{
    if (this->getPositionX() <= 1340)
    {
        soundManager::getIns()->sfx(1);
        this->setPositionX(-500);
    }
}

Rect enemy1::getBox()
//충돌 박스 보정 함수
{
    Sprite* en1 = (Sprite*)this->getChildByName("en1");
    Rect rt = en1->getBoundingBox();
    rt.origin += this->getPosition();

    return rt;
}
