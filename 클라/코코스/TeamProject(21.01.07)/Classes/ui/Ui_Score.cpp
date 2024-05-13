#include "Ui_Score.h"

static Ui_Score* obj = 0;

bool Ui_Score::init()
{
    finalScore = 0;
    checkLabel = true;

    Label* score = Label::createWithTTF("00000", "fonts/Recipekorea_FONT.ttf", 50);
    this->addChild(score);
    score->setPosition(Vec2(1220, 670));
    score->setAnchorPoint(Vec2(1, 0.5));
    score->setName("score");

    this->scheduleUpdate();

    return true;
}

void Ui_Score::scoreUp(int score)
{
    Label* s = (Label*)this->getChildByName("score");
    finalScore += score;
    std::string str = StringUtils::format("%05d", finalScore);
    s->setString(str);
}

void Ui_Score::update(float dt)
{
    //this->getChildByName("s") == NULL
    if (finalScore > 3000) {
        if (checkLabel==true) {
            Label* s = Label::createWithTTF("Guided missile unlock!!", "fonts/Recipekorea_FONT.ttf", 50);
            this->addChild(s);
            s->setPosition(Vec2(640, 360));
            s->setTextColor(Color4B::YELLOW);

            DelayTime* dt = DelayTime::create(1);
            FadeOut* out = FadeOut::create(1);
            //RemoveSelf* self = RemoveSelf::create(true);
            Sequence* seq = Sequence::create(dt, out, 0);
            s->runAction(seq);
            checkLabel = false;
        }
    }
}

void Ui_Score::reset()
{
    finalScore = 0;
    checkLabel = true;

    Label* s = (Label*)this->getChildByName("score");
    std::string str = StringUtils::format("00000");
    s->setString(str);

    this->unscheduleUpdate();
    this->scheduleUpdate();
}

Ui_Score* Ui_Score::getIns()
{
    if (obj == 0) {
        obj = Ui_Score::create();
        obj->retain();
    }
    return obj;
}
