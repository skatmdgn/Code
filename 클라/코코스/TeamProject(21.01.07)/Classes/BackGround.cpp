#include "BackGround.h"

bool BackGround::init()
{
    Sprite* bg_stop = Sprite::create("BKG_stop_1.png");
    this->addChild(bg_stop);
    bg_stop->setZOrder(-4);
    bg_stop->setAnchorPoint(Vec2(0, 0));
    bg_stop->setContentSize(Size(1280, 720));
   
    Sprite* bg_move1 = Sprite::create("BKG_move_1.png");
    Sprite* bg_move2 = Sprite::create("BKG_move_2.png");
    Sprite* bg_move3 = Sprite::create("BKG_move_3.png");
    
    this->addChild(bg_move1);
    this->addChild(bg_move2);
    this->addChild(bg_move3);

    bg_move1->setZOrder(-1);
    bg_move2->setZOrder(-2);
    bg_move3->setZOrder(-3);

    bg_move1->setAnchorPoint(Vec2(0, 0));
    bg_move2->setAnchorPoint(Vec2(0, 0));
    bg_move3->setAnchorPoint(Vec2(0, 0));

    bg_move1->setTag(1);
    bg_move2->setTag(2);
    bg_move3->setTag(3);

    Sprite* bg_move1_2 = Sprite::create("BKG_move_1.png");
    Sprite* bg_move2_2 = Sprite::create("BKG_move_2.png");
    Sprite* bg_move3_2 = Sprite::create("BKG_move_3.png");

    bg_move1_2->setPositionX(955);
    bg_move2_2->setPositionX(955);
    bg_move3_2->setPositionX(955);

    this->addChild(bg_move1_2);
    this->addChild(bg_move2_2);
    this->addChild(bg_move3_2);

    bg_move1_2->setZOrder(-1);
    bg_move2_2->setZOrder(-2);
    bg_move3_2->setZOrder(-3);

    bg_move1_2->setAnchorPoint(Vec2(0, 0));
    bg_move2_2->setAnchorPoint(Vec2(0, 0));
    bg_move3_2->setAnchorPoint(Vec2(0, 0));

    bg_move1_2->setTag(4);
    bg_move2_2->setTag(5);
    bg_move3_2->setTag(6);

    Sprite* bg_move1_3 = Sprite::create("BKG_move_1.png");
    Sprite* bg_move2_3 = Sprite::create("BKG_move_2.png");
    Sprite* bg_move3_3 = Sprite::create("BKG_move_3.png");

    bg_move1_3->setPositionX(1910);
    bg_move2_3->setPositionX(1910);
    bg_move3_3->setPositionX(1910);

    this->addChild(bg_move1_3);
    this->addChild(bg_move2_3);
    this->addChild(bg_move3_3);

    bg_move1_3->setZOrder(-1);
    bg_move2_3->setZOrder(-2);
    bg_move3_3->setZOrder(-3);

    bg_move1_3->setAnchorPoint(Vec2(0, 0));
    bg_move2_3->setAnchorPoint(Vec2(0, 0));
    bg_move3_3->setAnchorPoint(Vec2(0, 0));

    bg_move1_3->setTag(7);
    bg_move2_3->setTag(8);
    bg_move3_3->setTag(9);

    speed1 = 300;
    speed2 = 200;
    speed3 = 100;

    this->scheduleUpdate();

    return true;
}

void BackGround::update(float dt)
{
    Node* pSpr1 = this->getChildByTag(1);
    Node* pSpr2 = this->getChildByTag(2);
    Node* pSpr3 = this->getChildByTag(3);
    
    pSpr1->setPositionX(pSpr1->getPositionX() - dt * speed1);
    pSpr2->setPositionX(pSpr2->getPositionX() - dt * speed2);
    pSpr3->setPositionX(pSpr3->getPositionX() - dt * speed3);

    Node* pSpr1_2 = this->getChildByTag(4);
    Node* pSpr2_2 = this->getChildByTag(5);
    Node* pSpr3_2 = this->getChildByTag(6);

    pSpr1_2->setPositionX(pSpr1_2->getPositionX() - dt * speed1);
    pSpr2_2->setPositionX(pSpr2_2->getPositionX() - dt * speed2);
    pSpr3_2->setPositionX(pSpr3_2->getPositionX() - dt * speed3);

    Node* pSpr1_3 = this->getChildByTag(7);
    Node* pSpr2_3 = this->getChildByTag(8);
    Node* pSpr3_3 = this->getChildByTag(9);

    pSpr1_3->setPositionX(pSpr1_3->getPositionX() - dt * speed1);
    pSpr2_3->setPositionX(pSpr2_3->getPositionX() - dt * speed2);
    pSpr3_3->setPositionX(pSpr3_3->getPositionX() - dt * speed3);
    
    if (pSpr1->getPositionX() < -959)
    {
        pSpr1->setPositionX(1910);
    }
    if (pSpr1_2->getPositionX() < -959)
    {
        pSpr1_2->setPositionX(1910);
    }
    if (pSpr1_3->getPositionX() < -959)
    {
        pSpr1_3->setPositionX(1910);
    }
    if (pSpr2->getPositionX() < -959)
    {
        pSpr2->setPositionX(1910);
    }
    if (pSpr2_2->getPositionX() < -959)
    {
        pSpr2_2->setPositionX(1910);
    }
    if (pSpr2_3->getPositionX() < -959)
    {
        pSpr2_3->setPositionX(1910);
    }
    if (pSpr3->getPositionX() < -959)
    {
        pSpr3->setPositionX(1910);
    }
    if (pSpr3_2->getPositionX() < -959)
    {
        pSpr3_2->setPositionX(1910);
    }
    if (pSpr3_3->getPositionX() < -959)
    {
        pSpr3_3->setPositionX(1910);
    }
}
