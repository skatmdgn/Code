#include "bulletManager.h"
#include "enemies/eBullet.h"
#include "laser/laser1.h"
#include "laser/laser2.h"

static bulletManager* obj = 0;

bool bulletManager::init()
{
    for (int i = 0; i < 10; i++)
    {
        eBullet* eb = eBullet::create();
        this->addChild(eb);
        b_e_vec_wait.pushBack((Node*)eb);
        eb->setZOrder(3);
    }

    this->scheduleUpdate();

    return true;
}

void bulletManager::update(float dt)
{
    for (Node* laser : b_p_vec1)
    {
        laser1* l = (laser1*)laser;
        if (l->getPositionX() > 1300) {
            b_p_vec1_wait.pushBack((Node*)l);
            b_p_vec1.eraseObject(l);
            l->laser1_DisAct();
            return;
        }
    }
    //饭捞历1 拳搁观 贸府
    for (Node* laser : b_p_vec2)
    {
        laser2* l = (laser2*)laser;
        if (l->getPositionX() > 1300) {
            b_p_vec2_wait.pushBack((Node*)l);
            b_p_vec2.eraseObject(l);
            l->laser2_DisAct();
            return;
        }
    }
    //饭捞历2 拳搁观 贸府

    for (Node* Bullet : b_e_vec)
    {
        eBullet* e = (eBullet*)Bullet;
        if (e->getPositionX() <= -35) {
            e->setScale(1);
            e->speed = 500;
            b_e_vec_wait.pushBack((Node*)e);
            b_e_vec.eraseObject(e);
            e->eb_DisAct();

            return;
        }
    }
    //阂复 拳搁观 贸府
}

void bulletManager::reset()
{
    for (Node* e : b_e_vec) {
        e->removeFromParentAndCleanup(true);
        //b_e_vec_wait.pushBack(e);
        //e->setPosition(Vec2(999, 999));
    }
    for (Node* e : b_p_vec1) {
        e->removeFromParentAndCleanup(true);
        //b_p_vec1_wait.pushBack(e);
        //e->setPosition(Vec2(999, 999));
    }
    for (Node* e : b_p_vec2) {
        e->removeFromParentAndCleanup(true);
        //b_p_vec2_wait.pushBack(e);
        //e->setPosition(Vec2(999, 999));
    }
    for (Node* e : b_p_vec1_wait) {
        e->removeFromParentAndCleanup(true);
    }
    for (Node* e : b_p_vec2_wait) {
        e->removeFromParentAndCleanup(true);
    }
    for (Node* e : b_e_vec_wait) {
        e->removeFromParentAndCleanup(true);
    }

    b_p_vec2.clear();
    b_e_vec.clear();
    b_p_vec1.clear();
    b_p_vec1_wait.clear();
    b_p_vec2_wait.clear();
    b_e_vec_wait.clear();
}

bulletManager* bulletManager::getIns()
{
    if (obj == 0)
    {
        obj = bulletManager::create();
        obj->retain();
    }
    return obj;
}