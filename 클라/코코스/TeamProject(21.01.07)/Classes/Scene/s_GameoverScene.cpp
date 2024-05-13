#include "s_GameScene.h"
#include "s_GameOverScene.h"
#include "s_StartScene.h"
#include "soundManager.h"

bool s_GameoverScene::init()
{
    soundManager::getIns()->bgm(3);

    Label* Die = Label::createWithTTF("Game Over", "fonts/Recipekorea_FONT.ttf", 100);
    this->addChild(Die);
    Die->setTextColor(Color4B(255, 215, 0, 255));
    Die->setPosition(Vec2(640, 550));

    Label* Again = Label::createWithTTF("Play Again?", "fonts/Recipekorea_FONT.ttf", 60);
    this->addChild(Again);
    Again->setTextColor(Color4B(255, 255, 255, 255));
    Again->setPosition(Vec2(640, 400));

    Label* yes = Label::createWithTTF("Yes", "fonts/Recipekorea_FONT.ttf", 40);
    this->addChild(yes);
    yes->setTextColor(Color4B(248, 155, 0, 255));
    yes->setPosition(Vec2(400, 200));
    yes->setTag(1);

    Label* no = Label::createWithTTF("No", "fonts/Recipekorea_FONT.ttf", 40);
    this->addChild(no);
    no->setTextColor(Color4B(248, 155, 0, 255));
    no->setPosition(Vec2(900, 200));
    no->setTag(2);

    nowBlk = true;
    
    next();

    EventListenerKeyboard* key = EventListenerKeyboard::create();
    key->onKeyPressed = CC_CALLBACK_2(s_GameoverScene::onKeyPress, this);
    _eventDispatcher->addEventListenerWithSceneGraphPriority(key, this);

    return true;
}

void s_GameoverScene::onKeyPress(EventKeyboard::KeyCode key, Event* e)
{
    if (key==EventKeyboard::KeyCode::KEY_R)
    {
        soundManager::getIns()->stopA();
        s_StartScene* sc = s_StartScene::create();
        Director::getInstance()->replaceScene(sc);
    }
    if (key == EventKeyboard::KeyCode::KEY_RIGHT_ARROW || key == EventKeyboard::KeyCode::KEY_LEFT_ARROW)
    {
        next();
    }
    if (key == EventKeyboard::KeyCode::KEY_ENTER)
    {
        soundManager::getIns()->stopA();
        if (nowBlk == true)
        {
            s_StartScene* sc = s_StartScene::create();
            Director::getInstance()->replaceScene(sc);
        }
        else
        {
            s_GameScene* sc = s_GameScene::create();
            Director::getInstance()->replaceScene(sc);
        }
    }
}

void s_GameoverScene::next()
{
    Label* now1 = (Label*)this->getChildByTag(1);
    Label* now2 = (Label*)this->getChildByTag(2);
    now1->stopAllActions();
    now2->stopAllActions();

    now1->setVisible(true);
    now2->setVisible(true);

    Blink* blk = Blink::create(2, 5);
    RepeatForever* rep = RepeatForever::create(blk);

    if (nowBlk == true)
    {
        now1->runAction(rep);
        nowBlk = false;
    }
    else if (nowBlk == false)
    {
        now2->runAction(rep);
        nowBlk = true;
    }
}


