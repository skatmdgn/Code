#pragma once
#include <cocos2d.h>
USING_NS_CC;

class eBullet:public Node
{
public:
	bool init();
	CREATE_FUNC(eBullet);

	float speed;
	Vec2 dir;

	void update(float dt), eb_Active(Vec2 v), eb_DisAct(), damage(), scale();

	Rect getBox();
};

