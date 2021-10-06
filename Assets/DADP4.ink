VAR job = 5
VAR family = 5

->WakeUp

//if possible, instead of cliking on choices, player clicks on in world objects e.g. if choice is go home/stay at office, those objects can be highlighted and the player clicks on the one they want

===WakeUp===
Yawn... //trigger animation of player waking up 
//ping sound and/or email bar shown with another notification
It's 8:00am and my partner is still in bed. Should I #speech #main #continue
# world # oven # computer
***make breakfast
->breakfast
***answer my emails over a quick coffee at the office? 
-> emails
#world #computer 




//cooking animation
===breakfast===
Oh hey! you're up. #speech #main
Is that breakfast I smell? oh wow you didn't have to do that. #speech #spouse
I know, but I wanted to. #speech #main
~family += 1
~job -= 1
//eating animation
I should go to work now #world #computor
*** get back to work
-> emails //only triggered when player is at work


===emails===
//email pops up on screen?
Oh wow, they actually responded to my email! #speech #main
//screenshot of email - positive response to request for mentorship 
//maybe communication with mentor only talks place through email (screenshots)
I would be interested in mentoring you. I am very busy would you be able to zoom call in 30 minutes? #speech #computor #noclear //email
~job += 1
Honey, would you like to eat lunch together? #speech #spouse #noclear//partner (call) --> just using parnter for now
#speech #main
***I would love to
but I need to be back at the office within thirty minutes #speech #main
Thats plenty of time! //partner
->home

***I'm sorry I can't
I just got this amazing mentorship opportunity and I have to prep for the meeting, sorry.
No that's fine I understand. #speech #spouse //sad speechbubble (partner)
~family -= 1
->work
#world #computer

==work===
///animation of screen typing
Ahh, I am so prepared for thus meeting! #sppech #main
I should go to my zoom now //trying to prompt the player to the computer
~job += 1
//zoom call animation/something to imply passage of time
That went really well! #speech #main
It's quite late #speech #main
#world home #computor
***go home and celebrate with my family or 
~family += 1
#world #home
->gohome2
***Stay and work for a couple more hours  #speech #main 
#world #work
->gooffice
</world>


===gohome2===
Hey, how was your day #speech #spouse
Absolutely amazing #speech #main
Would you like to talk about it? #speech #spouse
***yes 
-> yes2
***no  #speech #main 
->no

===yes2===
{ family > 5: 
***I would never have aced this interview without your support!#speech #main 
#world #home
->endEvent1 
-> Thank
***This happened because of me #speech #main 
->responsibility2
#world #home
->endEvent1 
} 
{family < 6: 
My hard work really paid off. I did not think that I would be able to do this alone but I am happy I did. #speech #main 
}



===Thank===
~family += 1
#world #home
->endEvent1 

===responsibility2===
My hard work really paid off. I did not think that I would be able to do this alone but I am happy I did. #speech #main 
#world #home
->endEvent1 

{ family>4: 
//animation of couple talking
I'm so happy that I can talk to you #speech #main
~family += 1
#world #home
->endEvent1 
}

===gooffice===
~job += 1
~family -= 1
//person typing animation and/or clock
Wow it's already past midnight. I should go home #speech #main
//next scene only triggered when character walks home
#world #home
*** go to sleep
->endEvent1 //triggered by player walking to bed


===home===
eating animation
~family += 1
Yum.. thank you for the lovely meal.#speech # spouse
Oh no! I am late for my meeting, I should run back to the office now.
//when player presses enter key to interact with computer, email opens up:
Obviosuly you are busy, I think we should reschedule to next month #speech #computor//mentoring
~job -= 1
That's so disappointing, I should be more prepared next time # speech #main
#world #bed #computor
***go home or 
->gohome
***work more at the office #speech #main
#world #work
->gooffice


===gohome===
~family += 1
Hey, how was your day #speech #spouse
Absolutely terrible #speech #main
Would you like to talk about it? #partner #continue


#speech #main
***no
-> no
***yes #speech #main
->yes

===yes===
{ family < 8: //If the main character chose to fo to work instead of breakfast 
#speech #main
***I blame you
-> blame
***This happened because of me #speech #main
->responsibility
}

{ family>7: 
//animation of couple talking
I'm so happy that I can talk to you #speech spouse
~family += 1
#world #home
->endEvent1 
}

===blame===
//angry talking animation
This is all your fault! #speech #main
If you had not invited me for lunch. I would never have missed the meeeting! #speech #spouse
~family -= 1
#world #home
->endEvent1 

===responsibility===
I'm so happy that I can talk to you #speech #main
~family += 1
#world #home
->endEvent1 


===no=== //you want to talk about interested
~family -= 1
#world #home
->endEvent1 



***Stay and work for a couple more hours #world #work
~job += 1
~family -= 1
//zoom call/person typing
Wow it's already past midnight. I should go home
#world #home
->endEvent1


===endEvent1
//if job > 5 then expand maybe reset job to 0
//if work > 5 then expand and reset to 0
//if family < 5 then more inhuman looking, if < 3 - even more inhuman

-> END
