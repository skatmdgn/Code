#pragma once
#include <cocos2d.h>
USING_NS_CC;

//랜덤한 y 위치에서 그저 왼쪽으로 쭉 가는 에너미

class enemy1:public Node
{
public:
	bool init(), target = false;
	CREATE_FUNC(enemy1);

	void update(float dt), enemy1_Active(), enemy1_DisAct(), damage();
	Rect getBox();

	Vec2 dir;

	float ang = 0;
};

