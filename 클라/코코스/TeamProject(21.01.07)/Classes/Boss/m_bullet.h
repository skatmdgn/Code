#pragma once
#include <cocos2d.h>
USING_NS_CC;
class m_bullet:public Node
{
public:
	bool init();
	CREATE_FUNC(m_bullet);

	Vec2 dir;
	float speed;
	int kinds;

	void update(float dt);
	Rect GetBox();
};

