using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

namespace CtrlAltJam3
{
    public class CamsGeneretor : MonoBehaviour
    {

        [SerializeField] int camAmount,boardWidth;
        [SerializeField] Cams[] camBoard;
        [SerializeField] CamGroup[] cams;
        [SerializeField] string[] codes = new string[3];
        void Start ()
        {
            resetClass();
        }

        public void GenerateBoard()
        {
            for (int x = 0; x < boardWidth; x++) 
            { 
                Cams cam = new Cams();
                cam.position = x;
                cam.type = Cams.Type.EMPTY;
                cam.number = 0;
                camBoard[x] = cam;
            }
        }

        void GenerateCams()
        {
            int camGroupPosition = 0;
            int lastCounter = 0;
            int counter = 0;
            while (camGroupPosition < camAmount)
            {
                if(lastCounter == camGroupPosition) { counter++; } else { counter = 0; lastCounter = camGroupPosition; }
                if (counter == 20) break;
                int initialPosition = Random.Range(0, boardWidth-1);
                int size = Random.Range(1, 4);
                switch (size) 
                {
                    case >= 2:
                        if (initialPosition + size < boardWidth)
                        {
                            bool canCreateThere = true;
                            if (initialPosition == 0)
                            {
                                canCreateThere = camBoard[initialPosition + size].type == Cams.Type.EMPTY;
                            }
                            else
                            {
                                canCreateThere = camBoard[initialPosition + size].type == Cams.Type.EMPTY && camBoard[initialPosition - 1].type == Cams.Type.EMPTY;
                            }

                            if (canCreateThere)
                            {
                                cams[camGroupPosition].cams = new Cams[size];
                                bool canCreate = true;
                                for (int i = 0; i < size; i++)
                                {
                                    if (camBoard[initialPosition+i].type == Cams.Type.DISABLED)
                                    {
                                        canCreate = false;
                                        break;
                                    }
                                }
                                if(canCreate == true)
                                {
                                    Cams[] group = new Cams[size];
                                    for (int i = 0;i < size; i++)
                                    {
                                        Cams cam = new Cams();
                                        cam.position = initialPosition;
                                        cam.type = Cams.Type.DISABLED;
                                        camBoard[initialPosition+i] = cam;
                                        group[i] = cam;;
                                    }
                                    cams[camGroupPosition].cams = group;
                                    cams[camGroupPosition].code = GeneratedCode();
                                    camGroupPosition++;
                                    break;
                                }
                                break;
                            }
                        }
                        break;
                    case 1:
                        bool canCreateHere = true;
                        if(initialPosition == 0)
                        {
                            canCreateHere = camBoard[initialPosition + 1].type == Cams.Type.EMPTY;
                        }
                        else
                        {
                            canCreateHere = camBoard[initialPosition + 1].type == Cams.Type.EMPTY && camBoard[initialPosition - 1].type == Cams.Type.EMPTY;
                        }

                        if (canCreateHere)
                        {
                            Cams cam = new Cams();
                            cam.position = initialPosition;
                            cam.type = Cams.Type.DISABLED;
                            camBoard[initialPosition] = cam;
                            cams[camGroupPosition].cams = new Cams[] { cam };
                            cams[camGroupPosition].code = GeneratedCode();
                            camGroupPosition++;
                        }
                        break;
                    default:
                        break;
                }
            }
            Debug.Log("finalizou");

        }

        private void resetClass()
        {
            cams = new CamGroup[camAmount];
            camBoard = new Cams[boardWidth];
            GenerateBoard();
        }

        public CamBoard GetBoard()
        {
            GenerateBoard();
            GenerateCams();

            CamBoard board = new CamBoard();
            board.board = camBoard;
            board.cams = cams;
            resetClass();
            return board;
        }

        public string GeneratedCode()
        {
            int a = Random.Range(0, 3);
            int b = Random.Range(0, 3);
            int c = Random.Range(0, 3);
            string result = "";
            result = result + codes[0].ToCharArray()[a];
            result = result + codes[1].ToCharArray()[b];
            result = result + codes[2].ToCharArray()[c];
            Debug.Log(result);
            return result;
        }

        public void updateGenerator(int _camAmount, int _boardWidth)
        {
            this.camAmount = _camAmount;
            this.boardWidth = _boardWidth;
            resetClass();
        }

    }
}
