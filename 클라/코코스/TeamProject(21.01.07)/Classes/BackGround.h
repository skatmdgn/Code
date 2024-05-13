#pragma once
#include<cocos2d.h>
USING_NS_CC;
class BackGround :public Layer
{
public:
	bool init();
	void update(float dt);

	float speed1;
	float speed2;
	float speed3;

	CREATE_FUNC(BackGround);
};

