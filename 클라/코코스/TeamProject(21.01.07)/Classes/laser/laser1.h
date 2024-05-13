#pragma once
#include <cocos2d.h>
USING_NS_CC;

class laser1 :public Node
{
public:
	bool init();
	CREATE_FUNC(laser1);
	void update(float dt);

	void laser1_Active(), laser1_DisAct();

	void collision();

	Rect getBox();
};


