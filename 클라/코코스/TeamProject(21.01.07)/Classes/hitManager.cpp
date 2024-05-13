#include "hitManager.h"
#include "Boss/Boss.h"
#include "laser/laser1.h"
#include "laser/laser2.h"
#include "ui/Ui_hpicon.h"
#include "ui/Ui_Score.h"

static hitManager* obj = 0;

bool hitManager::init()
{
    this->scheduleUpdate();

	return true;
}


void hitManager::Playerenemy()
{
    if (em->e1_vec.empty() != true)
    {
        for (Node* p : em->e1_vec)
        {
            enemy1* e = (enemy1*)p;
            Rect r1 = play->getBox();
            Rect r2 = e->getBox();
            if (r1.intersectsRect(r2) == true)
            {
                Ui_hpicon::getIns()->damage();
                play->damage();
                break;
            }
        }
    }
    if (em->e2_vec.empty() != true)
    {
        for (Node* p : em->e2_vec)
        {
            enemy2* e = (enemy2*)p;
            Rect r1 = play->getBox();
            Rect r2 = e->getBox();
            if (r1.intersectsRect(r2) == true)
            {
                Ui_hpicon::getIns()->damage();
                play->damage();
                break;
            }
        }
    }
}

void hitManager::Playerboss()
{
    Node* parent = this->getParent();
    Boss* boss = (Boss*)parent->getChildByName("Boss");
    if (boss != NULL)
    {
        Rect r1 = play->getBox();
        Rect r2 = boss->GetBox();
        if (r1.intersectsRect(r2) == true)
        {
            Ui_hpicon::getIns()->damage();
            play->damage();
        }
    }
}

void hitManager::PlayerBullet()
{
    if (bullet->b_e_vec.empty() != true)
    {
        for (Node* b : bullet->b_e_vec)
        {
            eBullet* bul = (eBullet*)b;
            Rect r1 = play->getBox();
            Rect r2 = bul->getBox();
            if (r1.intersectsRect(r2) == true)
            {
                Ui_hpicon::getIns()->damage();
                play->damage();
                bul->damage();
                break;
            }
        }
    }
}

void hitManager::Enemylaser()
{
    if (bullet->b_p_vec1.empty() != true)
    {
        for (Node* b : bullet->b_p_vec1)
        {
            if (em->e1_vec.empty() != true)
            {
                for (Node* p : em->e1_vec)
                {
                    laser1* Pbul = (laser1*)b;
                    enemy1* e = (enemy1*)p;
                    Rect r1 = Pbul->getBox();
                    Rect r2 = e->getBox();
                    if (r1.intersectsRect(r2) == true)
                    {
                        //enemy1의 점수는 30점
                        Ui_Score::getIns()->scoreUp(30);
                        Pbul->setPosition(Vec2(9999, 9999));
                        e->damage();
                    }
                }
            }
            if (em->e2_vec.empty() != true)
            {
                for (Node* p : em->e2_vec)
                {
                    laser1* Pbul = (laser1*)b;
                    enemy2* e = (enemy2*)p;
                    Rect r1 = Pbul->getBox();
                    Rect r2 = e->getBox();
                    if (r1.intersectsRect(r2) == true)
                    {
                        //enemy2의 점수는 50점
                        Ui_Score::getIns()->scoreUp(50);
                        Pbul->setPosition(Vec2(9999, 9999));
                        e->damage();
                    }
                }
            }
        }
    }
    if (bullet->b_p_vec2.empty() != true)
    {
        for (Node* b : bullet->b_p_vec2)
        {
            if (em->e1_vec.empty() != true)
            {
                for (Node* p : em->e1_vec)
                {
                    laser2* Pbul = (laser2*)b;
                    enemy1* e = (enemy1*)p;
                    Rect r1 = Pbul->getBox();
                    Rect r2 = e->getBox();
                    if (r1.intersectsRect(r2) == true)
                    {
                        //enemy1의 점수는 30점
                        Ui_Score::getIns()->scoreUp(30);
                        Pbul->setPosition(Vec2(9999, 9999));
                        e->damage();
                    }
                }
            }
            if (em->e2_vec.empty() != true)
            {
                for (Node* p : em->e2_vec)
                {
                    laser2* Pbul = (laser2*)b;
                    enemy2* e = (enemy2*)p;
                    Rect r1 = Pbul->getBox();
                    Rect r2 = e->getBox();
                    if (r1.intersectsRect(r2) == true)
                    {
                        //enemy2의 점수는 50점
                        Ui_Score::getIns()->scoreUp(50);
                        Pbul->setPosition(Vec2(9999, 9999));
                        e->damage();
                    }
                }
            }
        }
    }
}

void hitManager::Bosslaser()
{
    if (bullet->b_p_vec1.empty() != true)
    {
        for (Node* b : bullet->b_p_vec1)
        {
            laser1* Pbul = (laser1*)b;
            Boss* boss = (Boss*)this->getParent()->getChildByName("Boss");
            if (boss != NULL) {
                Rect r1 = Pbul->getBox();
                Rect r2 = boss->GetBox();
                if (r1.intersectsRect(r2) == true)
                {
                    Ui_Score::getIns()->scoreUp(10);
                    boss->Damage();
                    Pbul->setPositionX(9999);
                }
            }
        }
    }
    if (bullet->b_p_vec2.empty() != true)
    {
        for (Node* b : bullet->b_p_vec2)
        {
            laser2* Pbul = (laser2*)b;
            Boss* boss = (Boss*)this->getParent()->getChildByName("Boss");
            if (boss != NULL) {
                Rect r1 = Pbul->getBox();
                Rect r2 = boss->GetBox();
                if (r1.intersectsRect(r2) == true)
                {
                    boss->Damage();
                    Pbul->setPositionX(9999);
                }
            }
        }
    }

}


void hitManager::update(float dt)
{
    play = (Player*)this->getParent()->getChildByName("Player");
    if (play != NULL)
    {
        Playerboss();
        //플레이어와 보스의 충돌판정
        Playerenemy();
        //플레이어와 적의 충돌판정
        PlayerBullet();
        //플레이어와 적총알의 충돌판정
        Enemylaser();
        //적과 총알의 충돌판정
        Bosslaser();
        //보스와 총알의 충돌판정
    }
}




void hitManager::reset()
{
    unscheduleUpdate();
    scheduleUpdate();
}

hitManager* hitManager::getIns()
{
    if (obj == 0)
    {
        obj = hitManager::create();
        obj->retain();
    }
    return obj;
}
