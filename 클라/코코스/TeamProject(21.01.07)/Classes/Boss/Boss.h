#pragma once
#include <cocos2d.h>
USING_NS_CC;
class Boss :public Node
{
public:
	bool init();
	CREATE_FUNC(Boss);

	int speed;
	int Hp;
	float Time = 0;
	float shotTime =0;
	Vec2 dir;
	Vec2 target;
	bool Next;
	bool Die;
	bool particle;
	float DeadTime = 0;
	int now_speed;
	bool bulletNext;


	Rect GetBox();
	void Damage();
	void update(float dt);
	void change();
	void dirMax();
	void pattern_1();
	void pattern_2();
};

