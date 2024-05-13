#pragma once
#include <cocos2d.h>
USING_NS_CC;

class laser2 :public Node
{
public:
	bool init();
	CREATE_FUNC(laser2);
	void update(float dt);

	Node* target;
	Vec2 dir;

	Vec2 beforePos;

	void laser2_Active(), laser2_DisAct();

	void collision();

	Rect getBox();

	static laser2* getIns();
};

