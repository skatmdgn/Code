#include "s_StartScene.h"
#include "s_GameScene.h"
#include "Scene/s_HowScene.h"

bool s_StartScene::init()
{
    Sprite* bg_stop = Sprite::create("BKG_stop_1.png");
    this->addChild(bg_stop);
    bg_stop->setZOrder(-4);
    bg_stop->setAnchorPoint(Vec2(0, 0));
    bg_stop->setContentSize(Size(1280, 720));
    bg_stop->setOpacity(100);

    rabel();

    EventListenerKeyboard* key = EventListenerKeyboard::create();
    key->onKeyPressed = CC_CALLBACK_2(s_StartScene::onKeyPress, this);
    _eventDispatcher->addEventListenerWithSceneGraphPriority(key, this);

    Button* bt = Button::create("question.png");
    this->addChild(bt);
    bt->setPosition(Vec2(640, 370));
    bt->addTouchEventListener(CC_CALLBACK_2(s_StartScene::btnclk, this));

    Sprite* spr = Sprite::create("tapTick.png");
    this->addChild(spr);
    spr->setPosition(Vec2(680, 310));

    return true;
}

void s_StartScene::onKeyPress(EventKeyboard::KeyCode key, Event* e)
{
    s_GameScene* sc = s_GameScene::create();
    Director::getInstance()->replaceScene(sc);
}

void s_StartScene::rabel()
{
    Label* Name = Label::createWithTTF("Team Game", "fonts/Recipekorea_FONT.ttf", 150);
    this->addChild(Name);
    Name->setTextColor(Color4B(245, 245, 220, 255));
    Name->setPosition(Vec2(640, 550));

    Label* Press = Label::createWithTTF("Press", "fonts/Recipekorea_FONT.ttf", 50);
    this->addChild(Press);
    Press->setTextColor(Color4B(255, 255, 255, 255));
    Press->setPosition(Vec2(430, 200));

    Label* Any = Label::createWithTTF("Any Key", "fonts/Recipekorea_FONT.ttf", 50);
    this->addChild(Any);
    Any->setTextColor(Color4B(247, 230, 0, 255));
    Any->setPosition(Vec2(630, 200));

    Label* Start = Label::createWithTTF("To Start", "fonts/Recipekorea_FONT.ttf", 50);
    this->addChild(Start);
    Start->setTextColor(Color4B(255, 255, 255, 255));
    Start->setPosition(Vec2(860, 200));

    Blink* bl1 = Blink::create(4, 3);
    Blink* bl2 = Blink::create(4, 3);
    Blink* bl3 = Blink::create(4, 3);
    RepeatForever* rep1 = RepeatForever::create(bl1);
    RepeatForever* rep2 = RepeatForever::create(bl2);
    RepeatForever* rep3 = RepeatForever::create(bl3);

    Press->runAction(rep1);
    Any->runAction(rep2);
    Start->runAction(rep3);
}

void s_StartScene::btnclk(Ref* ref, Widget::TouchEventType type)
{
    s_HowScene* hs = s_HowScene::create();
    Director::getInstance()->replaceScene(hs);
}

