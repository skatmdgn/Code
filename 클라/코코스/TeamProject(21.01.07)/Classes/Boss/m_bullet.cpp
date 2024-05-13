#include "m_bullet.h"

bool m_bullet::init()
{
	Sprite* B1;

	B1 = Sprite::create("shot_1.png");

	this->addChild(B1);
	this->setName("bullet");
	speed = 300;
	dir = Vec2(0, 0);

	this->scheduleUpdate();

	return true;
}

void m_bullet::update(float dt)
{
	if (dir != Vec2(0,0)) {
		this->setPosition(this->getPosition() + dt * dir * speed);
	}
}

Rect m_bullet::GetBox()
{
	Sprite* pSpr = (Sprite*)this->getChildByTag(1);
	Rect rt = pSpr->getBoundingBox();
	rt.origin += this->getPosition();
	return rt;
}

