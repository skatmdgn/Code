#pragma once
#include <cocos2d.h>
USING_NS_CC;
class Player:public Node
{
public:


	int hp;
	int speed;
	bool isDamage;//적 개체와 충돌상태확인변수

	void player_Act();
	void player_DisAct();

	//플레이어 객체의 움직임 관리
	void Press(EventKeyboard::KeyCode key, Event* e);
	void Release(EventKeyboard::KeyCode key, Event* e);
	void update(float dt);
	bool up, down, right, left;

	//플레이어 객체의 hitBox
	Rect getBox();

	//플레이어 충돌함수
	void damage();

	void reset();

	static Player* getIns();

	void mouseDown(EventMouse* e);

private:
	bool init();
	CREATE_FUNC(Player);	
};

