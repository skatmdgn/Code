#include "enemyManager.h"

static enemyManager* obj = 0;

bool enemyManager::init()
{
    e1_timer = 0;
    e2_timer = 0;
    e2_y = 0;

    //에너미1 미리 생성
    for (int i = 0; i < 4; i++)
    {
        enemy1* en = enemy1::create();
        this->addChild(en);
        e1_vec_wait.pushBack(en);
        en->setZOrder(2);
    }

    //에너미2 미리 생성
    for (int i = 0; i < 4; i++)
    {
        enemy2* en = enemy2::create();
        this->addChild(en);
        e2_vec_wait.pushBack(en);
        en->setZOrder(1);
    }

    this->scheduleUpdate();

    return true;
}

void enemyManager::update(float dt)
{
    //에너미1 생성
    {
        e1_timer += dt;
        if (e1_timer >= 1.2f)
        {
            e1_timer -= 1.2f;
            if (e1_vec_wait.size() > 0)
                //벡터 안에 보관된 객체 중 가장 뒤에 있는 객체를 가져옴
            {
                enemy1* e = e1_vec_wait.back();
                e1_vec.pushBack(e);
                e1_vec_wait.eraseObject(e);
                e->enemy1_Active();
                return;
            }
            else
                //사용 대기중인 객체가 없는 경우 새로 만들어서 사용
            {
                enemy1* e = enemy1::create();
                this->addChild(e);
                e->setZOrder(2);
                e1_vec.pushBack(e);
                e->enemy1_Active();
            }
        }

        for (enemy1* e : e1_vec)
        {
            if (e->getPositionX() <= -95)
                //충돌하지 않고 화면 밖으로 나갈 경우 비활성화하고 대기 벡터에 다시 넣음
            {
                e1_vec_wait.pushBack(e);
                e1_vec.eraseObject(e);
                e->enemy1_DisAct();
                break;
            }
        }
    }

    //에너미2 생성
    {
        e2_timer += dt;
        if (e2_timer >= 4)
        {
            e2_timer -= 2;

            if (e2_vec_wait.size() > 0)
                //벡터 안에 보관된 객체 중 가장 뒤에 있는 객체를 가져옴
            {
                enemy2* e = e2_vec_wait.back();
                e2_vec.pushBack(e);
                e2_vec_wait.eraseObject(e);
                e->enemy2_Active(e2_y);
            }
            else
                //사용 대기중인 객체가 없는 경우 새로 만들어서 사용
            {
                enemy2* e = enemy2::create();
                this->addChild(e);
                e->setZOrder(1);
                e2_vec.pushBack(e);
                e->enemy2_Active(e2_y);

            }
            e2_y++;

            if (e2_y >= 4)
            {
                e2_y = 0;
                e2_timer = -12;
            }
        }

        for (enemy2* e : e2_vec)
        {
            if (e->getPositionY() >= 780)
                //충돌하지 않고 화면 밖으로 나갈 경우 비활성화하고 대기 벡터에 다시 넣음
            {
                e2_vec_wait.pushBack(e);
                e2_vec.eraseObject(e);
                e->enemy2_DisAct();
                break;
            }
        }
    }
}

void enemyManager::eM_DisAct()
{
    this->unscheduleUpdate();

    for (enemy1* e : e1_vec)
    {
        e1_vec_wait.pushBack(e);
        e->setPosition(Vec2(999, 999));
        e->unscheduleUpdate();
        e->enemy1_DisAct();
    }
  
    for (enemy2* e : e2_vec)
    {
        e2_vec_wait.pushBack(e);
        e->setPosition(Vec2(999, 999));
        e->unscheduleUpdate();
        e->enemy2_DisAct();
    }
   
    e1_vec.clear();
    //e1_vec_wait.clear();
    e2_vec.clear();
    //e2_vec_wait.clear();
}

void enemyManager::reset()
{
    e1_timer = 0;
    e2_timer = 0;
    e2_y = 0;

   

    this->scheduleUpdate();
}

enemyManager* enemyManager::getIns()
{
    if (obj == 0)
    {
        obj = enemyManager::create();
        obj->retain();
    }
    return obj;
}
