#include "Player.h"
#include "player_shoot.h"
#include "Scene/s_GameOverScene.h"
#include "soundManager.h"

static Player* obj = 0;

bool Player::init()
{
	isDamage = true;
	hp = 3;
	speed = 350;
	left = false;
	right = false;
	up = false;
	down = false;

    Sprite* player = Sprite::create("shot_1.png");
    this->addChild(player);
    player->setName("player");
	player->setPosition(Vec2(100, 360));
	player->setZOrder(2);
	player->setColor(Color3B::RED);

	Sprite* playout = Sprite::create("PLAYER_back.png");
	playout->setZOrder(1);
	this->addChild(playout);
	playout->setName("out");
	playout->setScale(0.7f);

	player_shoot* shoot = player_shoot::getIns();
	this->addChild(shoot);

	EventListenerKeyboard* km = EventListenerKeyboard::create();
	km->onKeyPressed = CC_CALLBACK_2(Player::Press, this);
	km->onKeyReleased = CC_CALLBACK_2(Player::Release, this);
	_eventDispatcher->addEventListenerWithSceneGraphPriority(km, this);

	this->scheduleUpdate();

    return true;
}

void Player::player_Act()
{
	isDamage = true;
}

void Player::player_DisAct()
{
	isDamage = false;
}

void Player::Press(EventKeyboard::KeyCode key, Event* e)
{
	if (key == EventKeyboard::KeyCode::KEY_A) {
		left = true;
	}
	else if (key == EventKeyboard::KeyCode::KEY_D) {
		right = true;
	}
	if (key == EventKeyboard::KeyCode::KEY_W) {
		up = true;
	}
	if (key == EventKeyboard::KeyCode::KEY_S) {
		down = true;
	}
}

void Player::Release(EventKeyboard::KeyCode key, Event* e)
{
	if (key == EventKeyboard::KeyCode::KEY_A) {
		left = false;
	}
	else if (key == EventKeyboard::KeyCode::KEY_D) {
		right = false;
	}
	if (key == EventKeyboard::KeyCode::KEY_W) {
		up = false;
	}
	if (key == EventKeyboard::KeyCode::KEY_S) {
		down = false;
	}
}

void Player::update(float dt)
{
	Sprite* out = (Sprite*)getChildByName("out");
	Sprite* play = (Sprite*)getChildByName("player");
	if (play->getPositionX() > 10&& left == true) {
		play->setPositionX(play->getPositionX() - speed * dt);
		out->setTexture("PLAYER_back.png");
	}
	if(play->getPositionX() <1270&& right == true){
		play->setPositionX(play->getPositionX() + speed * dt);
		out->setTexture("PLAYER_front.png");
	}
	if (play->getPositionY() > 10 && down == true) {
		play->setPositionY(play->getPositionY() - speed * dt);
	}
	if (play->getPositionY() < 710 && up == true) {
		play->setPositionY(play->getPositionY() + speed * dt);
	}

	
	out->setPosition(play->getPosition());
}

Rect Player::getBox()
{//Ãæµ¹ ¹Ú½º º¸Á¤
	Sprite* p = (Sprite*)this->getChildByName("player");
	Rect rt = p->getBoundingBox();
	rt.origin += this->getPosition();
	return rt;
}

void Player::damage()
{
	if (hp > 0 && isDamage == true) {
		hp--;
		soundManager::getIns()->sfx(4);
		//Ãæµ¹ ÈÄ ºþÂ¦ÀÓ È¿°ú¸¦ ÁÜ
		Sprite* p = (Sprite*)this->getChildByName("player");
		Blink* bnk = Blink::create(3, 6);

		CallFunc* dis = CallFunc::create(CC_CALLBACK_0(Player::player_DisAct, this));
		CallFunc* act = CallFunc::create(CC_CALLBACK_0(Player::player_Act, this));

		Sequence* seq = Sequence::create(dis, bnk, act, 0);
		p->runAction(seq);

		Sprite* out = (Sprite*)getChildByName("out");
		bnk = Blink::create(3, 6);
		out->runAction(bnk);
	}
	else if (hp <= 0) {
		s_GameoverScene* sc = s_GameoverScene::create();
		Director::getInstance()->replaceScene(sc);
	}
}

void Player::reset()
{
	Sprite* p = (Sprite*)this->getChildByName("player");
	p->setVisible(true);

	Sprite* o = (Sprite*)this->getChildByName("out");
	o->setVisible(true);

	isDamage = true;
	hp = 3;
	speed = 350;
	left = false;
	right = false;
	up = false;
	down = false;
	p->setPosition(Vec2(100, 360));

	this->scheduleUpdate();
}

Player* Player::getIns()
{
	if (obj == 0) {
		obj = Player::create();
		obj->retain();
	}
	return obj;
}

