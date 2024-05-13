#include "s_HowScene.h"
#include "Scene/s_StartScene.h"

bool s_HowScene::init()
{
    Label* Name = Label::createWithTTF("How To Play", "fonts/Recipekorea_FONT.ttf", 80);
    this->addChild(Name);
    Name->setTextColor(Color4B::WHITE);
    Name->setPosition(Vec2(640, 650));

    Label* l1 = Label::createWithTTF("이동: W A S D", "fonts/Recipekorea_FONT.ttf", 40);
    this->addChild(l1);
    l1->setTextColor(Color4B::WHITE);
    l1->setPosition(Vec2(100, 500));
    l1->setAnchorPoint(Vec2(0, 0.5));

    Label* l2 = Label::createWithTTF("공격: 마우스 왼쪽 버튼 클릭", "fonts/Recipekorea_FONT.ttf", 40);
    this->addChild(l2);
    l2->setTextColor(Color4B::WHITE);
    l2->setPosition(Vec2(100, 440));
    l2->setAnchorPoint(Vec2(0, 0.5));

    Label* l3 = Label::createWithTTF("플레이어 빨간 점에 맞으면 데미지", "fonts/Recipekorea_FONT.ttf", 40);
    this->addChild(l3);
    l3->setTextColor(Color4B::WHITE);
    l3->setPosition(Vec2(100, 380));
    l3->setAnchorPoint(Vec2(0, 0.5));

    Label* l4 = Label::createWithTTF("스코어 3000점 이상이면 유도탄 해금", "fonts/Recipekorea_FONT.ttf", 40);
    this->addChild(l4);
    l4->setTextColor(Color4B::WHITE);
    l4->setPosition(Vec2(100, 320));
    l4->setAnchorPoint(Vec2(0, 0.5));

    rabel();

    EventListenerKeyboard* key = EventListenerKeyboard::create();
    key->onKeyPressed = CC_CALLBACK_2(s_HowScene::onPress, this);
    _eventDispatcher->addEventListenerWithSceneGraphPriority(key, this);

    return true;
}

void s_HowScene::onPress(EventKeyboard::KeyCode key, Event* e)
{
    s_StartScene* ss = s_StartScene::create();
    Director::getInstance()->replaceScene(ss);
}

void s_HowScene::rabel()
{
    Label* Press = Label::createWithTTF("Press", "fonts/Recipekorea_FONT.ttf", 50);
    this->addChild(Press);
    Press->setTextColor(Color4B(255, 255, 255, 255));
    Press->setPosition(Vec2(430, 200));

    Label* Any = Label::createWithTTF("Any Key", "fonts/Recipekorea_FONT.ttf", 50);
    this->addChild(Any);
    Any->setTextColor(Color4B(247, 230, 0, 255));
    Any->setPosition(Vec2(630, 200));

    Label* Start = Label::createWithTTF("To Title", "fonts/Recipekorea_FONT.ttf", 50);
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