#pragma once
#include <cocos2d.h>
USING_NS_CC;
#include "enemies/enemyManager.h"
#include "Player/Player.h"
#include "bulletManager.h"

class hitManager:public Node
{
public:
	bool init();
	
	void update(float dt);
	void Playerenemy();
	void Playerboss();
	void PlayerBullet();
	void Enemylaser();
	void Bosslaser();

	Player* play;
	enemyManager* em = enemyManager::getIns();
	bulletManager* bullet = bulletManager::getIns();

	void reset();

	static hitManager* getIns();

private:
	CREATE_FUNC(hitManager);
};

