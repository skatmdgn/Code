#include "laser2.h"
#include "Player/Player.h"
#include "enemies/enemyManager.h"
#include "enemies/enemy1.h"
#include "enemies/enemy2.h"
#include "Boss/Boss.h"

static laser2* obj = 0;

bool laser2::init()
{
    Sprite* laser = Sprite::create("BULLET.png");
    this->addChild(laser);
    laser->setName("laser");
	laser2_DisAct();
	dir = Vec2(1, 0);
	target == NULL;

    return true;
}

void laser2::update(float dt)
{
	target = NULL;
	if (target == NULL) {
		for (enemy1* p : enemyManager::getIns()->e1_vec) {
			Vec2 v1 = p->getPosition();
			Vec2 v2 = this->getPosition();
			float dis = v1.distance(v2);
			if (p->getPositionX() < this->getPositionX()) {
				continue;
			}
			if (dis < 200) {
				if (target == NULL) {
					if(p->getPositionX()>150)
					target = p;
				}
			}
		}

		for (enemy2* p : enemyManager::getIns()->e2_vec) {
			Vec2 v1 = p->getPosition();
			Vec2 v2 = this->getPosition();
			float dis = v1.distance(v2);
			if (p->getPositionX() < this->getPositionX()) {
				continue;
			}
			if (dis < 200) {
				if (target == NULL) {
					target = p;
				}
			}
		}
		if (target == NULL)
		{
			Scene* par = Director::getInstance()->getRunningScene();
			Boss* bas = (Boss*)par->getChildByName("Boss");
			if (bas != NULL)
			{
				target = bas;
			}
		}

	}
	

	if (target != NULL) {
		if (target->getPositionX() <= this->getPositionX()+100)
		{
			target = NULL;
		}
		else {
			Vec2 finish = target->getPosition();
			Vec2 start = this->getPosition();
			dir = finish - start;
		}
	}
	else {
		Vec2 finish = this->getPosition() + Vec2(1500, 0);
		Vec2 start = this->getPosition();
		dir = finish - start;
	}

	Vec2 nor = dir.getNormalized();
	this->setPosition(this->getPosition() + nor * 600 * dt);
}

void laser2::laser2_Active()
{
    this->setPositionX(Player::getIns()->getChildByName("player")->getPositionX() + 40);
    this->setPositionY(Player::getIns()->getChildByName("player")->getPositionY()-20);

    this->setVisible(true);

    this->scheduleUpdate();
}

void laser2::laser2_DisAct()
{
    this->setVisible(false);

    this->unscheduleUpdate();
}

void laser2::collision()
{
    this->setPositionX(1300);
}

Rect laser2::getBox()
{
    Sprite* l = (Sprite*)this->getChildByName("laser");
    Rect rt = l->getBoundingBox();
    rt.origin += this->getPosition();

    return rt;
}

laser2* laser2::getIns()
{
	if (obj == 0)
	{
		obj = laser2::create();
	}
	return obj;
}
