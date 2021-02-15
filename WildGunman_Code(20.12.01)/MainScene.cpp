#include "MainScene.h"
#include "UpDoor.h"
#include "DownDoor.h"
#include "Mon1.h"
#include "GameOver.h"

#include "AudioEngine.h"
using namespace experimental;

bool MainScene::init()
{
	Sprite* bg = Sprite::create("background.png");
	this->addChild(bg);
	bg->setPosition(Vec2(256, 288));
	bg->setZOrder(1);

	/* 태그번호 도어 위 왼쪽 0 오른쪽 1
	도어 아래 왼쪽 2 오른쪽 3
	도어 가운데 4
	몬스터 위 왼쪽 5 오른쪽 6
	아래 왼쪽 7 오른쪽 8
	가운데 9 */

	for (int i = 0; i < 2; i++)
	{
		UpDoor* ud = UpDoor::create();
		Mon1* mon = Mon1::create();
		this->addChild(ud);
		this->addChild(mon);
		ud->setPosition(Vec2(112 + 288 * i, 381));
		mon->setPosition(Vec2(112 + 288 * i, 376));
		ud->setTag(i);
		mon->setTag(i + 5);
		ud->setZOrder(2);
		mon->setZOrder(0);
		Sprite* spr = (Sprite*)mon->getChildByName("mon");
		spr->setTexture("mon1_up.png");
		
	}
	for (int i = 0; i < 2; i++)
	{
		DownDoor* dd = DownDoor::create();
		Mon1* mon = Mon1::create();
		this->addChild(dd);
		this->addChild(mon);
		dd->setPosition(Vec2(96 + 320 * i, 213));
		mon->setPosition(Vec2(96 + 320 * i, 208)); //211
		dd->setTag(i + 2);
		mon->setTag(i + 7);
		dd->setZOrder(2);
		mon->setZOrder(0);
		Sprite* spr = (Sprite*)mon->getChildByName("mon");
		spr->setTexture("mon1_down.png");
	}

	Sprite* md = Sprite::create("door_mid.png");
	this->addChild(md);
	md->setPosition(Vec2(256, 199));
	md->setTag(4);
	md->setZOrder(2);

	Mon1* mon = Mon1::create();
	this->addChild(mon);
	mon->setPosition(Vec2(256, 195));
	mon->setTag(9);
	mon->setZOrder(0);

	EventListenerTouchOneByOne* t = EventListenerTouchOneByOne::create();
	t->onTouchBegan = CC_CALLBACK_2(MainScene::touchBegan, this);
	_eventDispatcher->addEventListenerWithSceneGraphPriority(t, this);

	Label* score = Label::createWithTTF("SCORE: 0000", "fonts/pixel-nes.otf", 20);
	this->addChild(score);
	score->setPosition(Vec2(420, 25));
	score->setName("score");

	Label* sta = Label::createWithTTF("TAP TO START", "fonts/pixel-nes.otf", 20);
	this->addChild(sta);
	sta->setZOrder(10);
	sta->setTextColor(Color4B::YELLOW);
	sta->setPosition(Vec2(256, 80));
	sta->setName("sta");

	Label* gm = Label::createWithTTF("GUNMAN: 0.00", "fonts/pixel-nes.otf", 20);
	this->addChild(gm);
	gm->setAnchorPoint(Vec2(0, 0.5));
	gm->setTextColor(Color4B::GREEN);
	gm->setPosition(Vec2(10, 25));
	gm->setName("gm");

	yu = Label::createWithTTF("YOU: 0.00", "fonts/pixel-nes.otf", 20);
	this->addChild(yu);
	yu->setAnchorPoint(Vec2(0, 0.5));
	yu->setTextColor(Color4B::GREEN);
	yu->setPosition(Vec2(10, 50));
	yu->setName("yu");

	Blink* bnk = Blink::create(3, 4);
	Sequence* seq = Sequence::create(bnk, 0);
	RepeatForever* rf = RepeatForever::create(seq);
	sta->runAction(rf);

	this->scheduleUpdate();

	return true;
}

void MainScene::update(float dt)
{
	if (tim == true)
	{
		yuatk += dt;
		st = StringUtils::format("YOU: %.2f", yuatk);
		yu->setString(st);
		if (monatk <= yuatk)
		{
			AudioEngine::play2d("Shoot.mp3");
			AudioEngine::play2d("GameOver.mp3");
			GameOver* go = GameOver::create();
			Director::getInstance()->replaceScene(go);
		}
	}
}

bool MainScene::touchBegan(Touch* t, Event* e)
{
	Sprite* sprdoor = (Sprite*)this->getChildByTag(doorRan);
	if (doorOpen == true)
	{
		AudioEngine::play2d("Waiting.mp3");
		Label* sta = (Label*)this->getChildByName("sta");
		sta->setOpacity(0);

		Label* gm = (Label*)this->getChildByName("gm");
		string str = StringUtils::format("GUNMAN: %.2f", monatk);
		gm->setString(str);

		float t = rand() % 5 + (float)1;

		DelayTime* dtime = DelayTime::create(t);
		CallFunc* cf = CallFunc::create(CC_CALLBACK_0(MainScene::sprOpen, this, doorRan));
		Sequence* seq = Sequence::create(dtime, cf, 0);
		sprdoor->runAction(seq);
		doorOpen = false;
	}
	for (int i = 5; i < 10; i++)
	{
		Mon1* mon = (Mon1*)this->getChildByTag(i);
		Rect rt = mon->getChildByName("mon")->getBoundingBox();

		rt.origin += mon->getPosition();
		Vec2 pos = Vec2(t->getLocation());

		if (rt.containsPoint(pos) == true && mon->monhit == true)
		{
			AudioEngine::stopAll();
			AudioEngine::play2d("EnemyHit.mp3");
			AudioEngine::play2d("Shoot.mp3");

			Sprite* spr = (Sprite*)mon->getChildByName("mon");
			spr->setTexture("mon1dead.png");
			if (i <= 6)
			{
				mon->setPositionY(mon->getPositionY() - 5);
			}
			else if (i <= 8)
			{
				mon->setPositionY(mon->getPositionY() + 3);
			}

			CallFunc* cf = CallFunc::create(CC_CALLBACK_0(MainScene::sprClose, this, doorRan));
			DelayTime* dtime = DelayTime::create(2);
			Sequence* seq = Sequence::create(dtime, cf, 0);
			sprdoor->runAction(seq);

			mon->monhit = false;
			tim = false;
			break;
		}
	}
	return true;
}

void MainScene::sprOpen(int x)
{
	AudioEngine::stopAll();
	AudioEngine::play2d("On.mp3");
	if (x == 4)
	{
		Sprite* spr = (Sprite*)this->getChildByTag(x);
		spr->setTexture("");
	}
	else if (x >= 2)
	{
		DownDoor* dd = (DownDoor*)this->getChildByTag(x);
		Sprite* spr = (Sprite*)dd->getChildByName("down");
		spr->setTexture("door_down_open.png");
	}
	else if (x >= 0)
	{
		UpDoor* ud = (UpDoor*)this->getChildByTag(x);
		Sprite* spr = (Sprite*)ud->getChildByName("up");
		spr->setTexture("door_up_open.png");
	}
	Mon1* mon = (Mon1*)this->getChildByTag(x + 5);
	mon->monhit = true;
	tim = true;
}

void MainScene::sprClose(int x)
{
	AudioEngine::play2d("Clear.mp3");
	scorenum += 10;
	Label* score = (Label*)this->getChildByName("score");
	string str = StringUtils::format("SCORE: %04d", scorenum);
	score->setString(str);

	if (x == 4)
	{
		Sprite* spr = (Sprite*)this->getChildByTag(x);
		spr->setTexture("door_mid.png");
	}
	else if (x >= 2)
	{
		DownDoor* dd = (DownDoor*)this->getChildByTag(x);
		Sprite* spr = (Sprite*)dd->getChildByName("down");
		spr->setTexture("door_down.png");
	}
	else if (x >= 0)
	{
		UpDoor* ud = (UpDoor*)this->getChildByTag(x);
		Sprite* spr = (Sprite*)ud->getChildByName("up");
		spr->setTexture("door_up.png");
	}

	Mon1* mon = (Mon1*)this->getChildByTag(x + 5);
	Sprite* sprmon = (Sprite*)mon->getChildByName("mon");
	if (x + 5 <= 6)
	{
		mon->setPositionY(mon->getPositionY() + 5);
		sprmon->setTexture("mon1_up.png");
	}
	else if (x + 5 <= 8)
	{
		mon->setPositionY(mon->getPositionY() - 3);
		sprmon->setTexture("mon1_down.png");
	}
	else
	{
		sprmon->setTexture("mon1.png");
	}

	CallFunc* cf = CallFunc::create(CC_CALLBACK_0(MainScene::reGame, this));
	DelayTime* dtime = DelayTime::create(2);
	Sequence* seq = Sequence::create(dtime, cf, 0);

	this->runAction(seq);
}

void MainScene::reGame()
{
	doorRan = rand() % 5;
	doorOpen = true;

	Label* sta = (Label*)this->getChildByName("sta");
	sta->setOpacity(255);

	st = StringUtils::format("YOU: 0.00");
	yu->setString(st);

	sta = (Label*)this->getChildByName("gm");
	string str = StringUtils::format("GUNMAN: 0.00");
	sta->setString(str);

	yuatk = 0;
	monatk = (rand() / (float)RAND_MAX) / 2 + 0.5;
}
