#include "Boss.h"
#include "laser/laser1.h"
#include "bulletManager.h"
#include "enemies/eBullet.h"
#include "Scene/s_ClearScene.h"
#include "soundManager.h"
#include "Player/Player.h"

bool Boss::init()
{
    Sprite* Boss = Sprite::create("Boss.png");
    this->addChild(Boss);
    Boss->setTag(1);
    Boss->setPositionY(-50);
    Boss->setScale(2);
    Boss->setName("Boss");

    target = Vec2(1000, 200);
    dir = Vec2(-1, 1);

    MoveBy* m1 = MoveBy::create(2, Vec2(-300,0));
    this->runAction(m1);

    bulletNext = false;
    Next = false;
    Die = false;
    particle = false;
    speed = 100;

    this->scheduleUpdate();

    Hp = 250;
    return true;
}

Rect Boss::GetBox()
{
    Sprite* pSpr = (Sprite*)this->getChildByName("Boss");
    if (pSpr != NULL) {
        Rect rt = pSpr->getBoundingBox();
        rt.origin += this->getPosition();
        return rt;
    }
}

void Boss::Damage()
{
    Hp--;
    soundManager::getIns()->sfx(0);
    if (Hp == 150)
    {
        change();
    }
    if (Hp <= 0)
    {
        Die = true;
    }
}

void Boss::update(float dt)
{
    Time += dt;
    speed += dt * 40;
    if(Die != true)
    {
        this->setPosition(this->getPosition() + dir * dt * now_speed);
        if (this->getPositionX() < 600) {
            dir += Vec2(2, 0);
        }
        if (this->getPositionX() > 1150)
        {
            dir += Vec2(-2, 0);
        }
        if (this->getPositionY() > 650)
        {
            dir += Vec2(0, -2);
        }
        if (this->getPositionY() < 150)
        {
            dir += Vec2(0, 2);
        }
        dirMax();
    }

    if (Die == true) {
        DeadTime += dt;
        if (particle == false) {
            ParticleSystem* par;
            par = ParticleExplosion::create();
            DelayTime* d = DelayTime::create(5);
            RemoveSelf* r = RemoveSelf::create(true);
            Sequence* seq = Sequence::create(d, r, 0);
            this->addChild(par);
            par->runAction(seq);
            par->setPosition(this->getChildByTag(1)->getPosition());
            particle = true;
            shotTime = -9999;
        }

        if(DeadTime > 5)
        {
            soundManager::getIns()->sfx(1);
            s_ClearScene* sc = s_ClearScene::create();
            Director::getInstance()->replaceScene(sc);
            this->removeFromParentAndCleanup(true);
        }
    }
    shotTime += dt;
    if (shotTime >= 2)
    {
        now_speed = 0;
        
        
        if (Next == true && bulletNext == false) {
            pattern_2();
            bulletNext = true;
            shotTime -= 2;
        }

        else if (Next == false||bulletNext == true)
        {
            pattern_1();
            bulletNext = false;
            shotTime -= 1;
        }
    }
    if (shotTime >= 0.5f)
    {
        now_speed = speed;
    }
}

void Boss::change()
{
    Sprite* pSpr = (Sprite*)this->getChildByTag(1);
    pSpr->setTexture("Boss_2.png");
    Next = true;
    Time = 1;
    shotTime = 0;
    dir = Vec2(-1, -1);
    speed = speed * 2;
}

void Boss::dirMax()
{
    if (dir.x > 1)
    {
        dir.x = 1;
    }
    else if (dir.x < -1)
    {
        dir.x = -1;
    }
    if (dir.y > 1)
    {
        dir.y = 1;
    }
    else if (dir.y < -1)
    {
        dir.y = -1;
    }
}

void Boss::pattern_1()
{
    
    for (float i = -1; i <= 1; i=i+0.2f) {
        if ((eBullet*)bulletManager::getIns()->b_e_vec_wait.size() > 0)
        {
            eBullet* e = (eBullet*)bulletManager::getIns()->b_e_vec_wait.back();
            bulletManager::getIns()->b_e_vec.pushBack(e);
            bulletManager::getIns()->b_e_vec_wait.eraseObject(e);
            e->eb_Active(this->getPosition());
            e->dir = Vec2(1, i);
        }
        else
        {
            eBullet* e = eBullet::create();
            this->getParent()->addChild(e);
            bulletManager::getIns()->b_e_vec.pushBack(e);
            e->eb_Active(this->getPosition());
            e->dir = Vec2(1, i);
        }
    }
}

void Boss::pattern_2()
{
    Player* play = Player::getIns();
    Sprite* p=(Sprite*)play->getChildByName("player");
    if ((eBullet*)bulletManager::getIns()->b_e_vec_wait.size() > 0)
    {
        eBullet* e = (eBullet*)bulletManager::getIns()->b_e_vec_wait.back();
        bulletManager::getIns()->b_e_vec.pushBack(e);
        bulletManager::getIns()->b_e_vec_wait.eraseObject(e);
        e->eb_Active(this->getPosition());
        Vec2 direct = e->getPosition() - p->getPosition();
        direct = direct.getNormalized();
        e->dir = direct;
        e->speed = 1400;
        e->scale();
    }
    else
    {
        eBullet* e = eBullet::create();
        this->getParent()->addChild(e);
        bulletManager::getIns()->b_e_vec.pushBack(e);
        e->eb_Active(this->getPosition());
        Vec2 direct = e->getPosition() - p->getPosition();
        direct = direct.getNormalized();
        e->dir = direct;
        e->speed = 1400;
        e->scale();
    }
}


