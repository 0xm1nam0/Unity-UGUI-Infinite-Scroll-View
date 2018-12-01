﻿using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


#region Chinar Icon

/*
##########################################################################################################################$
##########################################################################################################################$
##########################################################################################################################$
##########################################################################################################################$
##########################################################################################################################$
##########################################################################################################################$
##########################################################@$@#############################################################$
#######################################################&:     :&##########################################################$
#####################################################|           !########################################################$
##################################################@;               :@#####################################################$
################################################@;                   ;@###################################################$
###############################################|                       |##################################################$
#############################################@:                         '&################################################$
############################################$`                           .$###############################################$
###########################################%.                             .%##############################################$
##########################################$`                               `$#############################################$
#########################################&'                                 '&############################################$
##########################$.    :&#######!                                   !#######&:    .%#############################$
##########################&'       |####&'                                   '&####|       '&#############################$
###########################%.       :@##%.                                   .%##@:       .%##############################$
############################&'       ;##|                                     |##;       '&###############################$
##############################@:     `$#|                                     |#&`     :@#################################$
###################################@&&##%.                                    |##&&@######################################$
################$:.                 '|@#$`                                   `$#@|'                 .;$###################$
###############!                                                                                       !##################$
###############&'                                                                                     '&##################$
################%.                                                                                    %###################$
#################!                                                                                   !####################$
##################!                                                                                 !#####################$
###################|                                                                               |######################$
####################&'                                                                           '&#######################$
######################|                                                                         |#########################$
########################!                                                                     ;###########################$
##########################%.                                                               .|#############################$
############################@;                                                           ;@###############################$
####################@;       `$#$`                                                   `%#$`       ;@#######################$
####################%.       !#&'                         `;`                         `$#|       .%#######################$
#############################%.                          '&#&'                          .%################################$
###########################&'                           !#####!                           '&##############################$
##########################%.                          :@#$%%%$#@;                          .%#############################$
#########################|                          !###&'   '&###!                          |############################$
##########################@;                    .|######&'   '&######|.                    ;@#############################$
###############################$;`         '!&##########&'   '&##########&|'         `;&##################################$
########################################################&'   '&###########################################################$
########################################################&'   '&###########################################################$
########################################################&'   '&###########################################################$
########################################################&'   '&###########################################################$
########################################################&'   '&###########################################################$
########################################################&'   '&###########################################################$
########################################################&'   '&###########################################################$
#########################################################&$$$&############################################################$
##########################################################@@@#############################################################$
####################&$$$$$&######&;`%###################&'   '&###########################################################$
###############|.          %#####$` !###################&'   '&###########################################################$
#############;  .|###############$` !#####################################################################################$
###########&' `$#################$` !#####################################################################################$
###########; '&##################$`            :@########@: ;#########$`      .|########|         ;@#####@:    `$#########$
##########$` !###################$` ;########@: .%#######@: ;#######@:  !####|. '&##############@: `$###$` :@#############$
##########$` |###################$` ;#########%. |#######@: ;#######! `$######$` !#######|.         %###% .%##############$
###########; '&##################$` ;#########%. |#######@: ;#######; '&######&' !#####%. .%#####%  %###% .%##############$
###########@: `$#################$` ;#########%. |#######@: ;#######; '&######&' !#####; :@######%  %###% .%##############$
#############!   !###############$` ;#########%. |#######@: ;#######; '&######&' !#####| .%######%  %###% .%##############$
###############%`          %#####$` ;#########%. |#######@: ;#######; '&######&' !######%.          %###% .%##############$
##########################################################################################################################$
##########################################################################################################################$
##########################################################################################################################$
##########################################################################################################################$
##########################################################################################################################$
##########################################################################################################################$
*/

#endregion


/// <summary>
/// <para>作用：</para>
/// <para>作者：Chinar</para>
/// <para>创建日期：2018-07-25</para>
/// </summary>
public class ChinarContent : MonoBehaviour
{
    private RectTransform   contentRect;
    private GridLayoutGroup contentGridLayoutGroup;
    private ScrollRect      scrollRect;
    private int             factor = 1;
    private int             childCount;
    private float           tempRecord;
    private int             maxCount;
    private float spaceBetween
    {
        get { return contentGridLayoutGroup.cellSize.y + contentGridLayoutGroup.spacing.y; }
    }


    void Awake()
    {
        contentRect            = GetComponent<RectTransform>();
        contentGridLayoutGroup = GetComponent<GridLayoutGroup>();
        scrollRect             = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
    }


    void Start()
    {
        InitializeInformation();
        maxCount = childCount = transform.childCount;
        scrollRect.onValueChanged.AddListener(ChinarOnChangeValue);
        StartCoroutine(Wait());
    }


    void InitializeInformation()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponentInChildren<Text>().text = (i + 1).ToString();
        }
    }


    IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        contentGridLayoutGroup.enabled = false;
    }


    public void ChinarOnChangeValue(Vector2 value)
    {
        if (tempRecord > value.y)
        {
            if (contentRect.anchoredPosition.y > spaceBetween * factor) ScrollView(true, 0);
        }
        else
        {
            if (contentRect.anchoredPosition.y < spaceBetween * (factor - 1)) ScrollView(false, maxCount - 1);
        }

        tempRecord = value.y;
    }


    private void ScrollView(bool isUp, int startIndex)
    {
        for (int i = 0; i < contentGridLayoutGroup.constraintCount; i++)
        {
            transform.GetChild(startIndex).GetComponent<RectTransform>().anchoredPosition += new Vector2(0, (isUp ? -1 : 1) * spaceBetween * maxCount / contentGridLayoutGroup.constraintCount);
            transform.GetChild(startIndex).GetComponentInChildren<Text>().text            =  isUp ? (++childCount).ToString() : (int.Parse(transform.GetChild(maxCount - 1).GetComponentInChildren<Text>().text) - maxCount).ToString();
            transform.GetChild(startIndex).SetSiblingIndex(isUp ? maxCount - 1 : 0);
            if (!isUp) childCount--;
        }

        contentRect.GetComponent<RectTransform>().sizeDelta += new Vector2(0, (isUp ? 1 : -1) * spaceBetween);
        factor                                              += isUp ? 1 : -1;
    }
}