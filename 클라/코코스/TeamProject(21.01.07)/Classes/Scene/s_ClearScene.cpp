#include "s_ClearScene.h"
#include "Scene/s_StartScene.h"
#include "soundManager.h"

bool s_ClearScene::init()
{
    soundManager::getIns()->bgm(2);

    Label* clear = Label::createWithTTF("Stage Clear!", "fonts/Recipekorea_FONT.ttf", 100);
    this->addChild(clear);
    clear->setTextColor(Color4B(255, 215, 0, 255));
    clear->setPosition(Vec2(640, 550));

    Label* Press = Label::createWithTTF("Press", "fonts/Recipekorea_FONT.ttf", 50);
    this->addChild(Press);
    Press->setTextColor(Color4B(255, 255, 255, 255));
    Press->setPosition(Vec2(430, 200));

    Label* Any = Label::createWithTTF("Any Key", "fonts/Recipekorea_FONT.ttf", 50);
    this->addChild(Any);
    Any->setTextColor(Color4B(247, 230, 0, 255));
    Any->setPosition(Vec2(630, 200));

    Label* Start = Label::createWithTTF("to title", "fonts/Recipekorea_FONT.ttf", 50);
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

    EventListenerKeyboard* key = EventListenerKeyboard::create();
    key->onKeyPressed = CC_CALLBACK_2(s_ClearScene::onKeyPress, this);
    _eventDispatcher->addEventListenerWithSceneGraphPriority(key, this);


    return true;
}

void s_ClearScene::onKeyPress(EventKeyboard::KeyCode key, Event* e)
{
    soundManager::getIns()->stopA();
    s_StartScene* sc = s_StartScene::create();
    Director::getInstance()->replaceScene(sc);
}
