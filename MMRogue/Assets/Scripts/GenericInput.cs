using UnityEngine;
using System.Collections;

// Enums.
public enum GenericAxis { LX, LY, RX, RY, LT, RT, DX, DY }
public enum GenericButton { O, U, Y, A, LB, RB, L3, R3, LT, RT, DU, DD, DL, DR, START, SELECT, SYSTEM }
public enum GenericJoystick { LeftStick, RightStick, DPad }
public enum GenericTrigger { Left, Right }

public class GenericInput : MonoBehaviour
{
    public static float GetAxis(GenericAxis axis, OuyaPlayer p = OuyaPlayer.P01)
    {
        switch (axis)
        {
            case GenericAxis.LX:
                return OuyaInput.GetAxis(OuyaAxis.LX, p) + Input.GetAxis("LX");
                break;
            case GenericAxis.LY:
                return OuyaInput.GetAxis(OuyaAxis.LY, p) + Input.GetAxis("LY");
                break;
            case GenericAxis.RX:
                return OuyaInput.GetAxis(OuyaAxis.RX, p) + Input.GetAxis("RX");
                break;
            case GenericAxis.RY:
                return OuyaInput.GetAxis(OuyaAxis.RY, p) + Input.GetAxis("RY");
                break;
            case GenericAxis.LT:
                return OuyaInput.GetAxis(OuyaAxis.LT, p) + Input.GetAxis("LT");
                break;
            case GenericAxis.RT:
                return OuyaInput.GetAxis(OuyaAxis.RT, p) + Input.GetAxis("RT");
                break;
            case GenericAxis.DX:
                return OuyaInput.GetAxis(OuyaAxis.DX, p) + Input.GetAxis("DX");
                break;
            case GenericAxis.DY:
                return OuyaInput.GetAxis(OuyaAxis.DY, p) + Input.GetAxis("DY");
                break;
            default:
                return 0;
                break;
        }
    }

    public static bool GetButtonDown(GenericButton button, OuyaPlayer p = OuyaPlayer.P01)
    {
        switch (button)
        {
            case GenericButton.O:
                return OuyaInput.GetButtonDown(OuyaButton.O, p) || Input.GetButtonDown("O");
                break;
            case GenericButton.U:
                return OuyaInput.GetButtonDown(OuyaButton.U, p) || Input.GetButtonDown("U");
                break;
            case GenericButton.Y:
                return OuyaInput.GetButtonDown(OuyaButton.Y, p) || Input.GetButtonDown("Y");
                break;
            case GenericButton.A:
                return OuyaInput.GetButtonDown(OuyaButton.A, p) || Input.GetButtonDown("A");
                break;
            case GenericButton.LB:
                return OuyaInput.GetButtonDown(OuyaButton.LB, p) || Input.GetButtonDown("LB");
                break;
            case GenericButton.RB:
                return OuyaInput.GetButtonDown(OuyaButton.RB, p) || Input.GetButtonDown("RB");
                break;
            case GenericButton.L3:
                return OuyaInput.GetButtonDown(OuyaButton.L3, p) || Input.GetButtonDown("L3");
                break;
            case GenericButton.R3:
                return OuyaInput.GetButtonDown(OuyaButton.R3, p) || Input.GetButtonDown("R3");
                break;
            case GenericButton.LT:
                return OuyaInput.GetButtonDown(OuyaButton.LT, p) || Input.GetButtonDown("LT");
                break;
            case GenericButton.RT:
                return OuyaInput.GetButtonDown(OuyaButton.RT, p) || Input.GetButtonDown("RT");
                break;
            case GenericButton.DU:
                return OuyaInput.GetButtonDown(OuyaButton.DU, p) || Input.GetButtonDown("DU");
                break;
            case GenericButton.DD:
                return OuyaInput.GetButtonDown(OuyaButton.DD, p) || Input.GetButtonDown("DD");
                break;
            case GenericButton.DL:
                return OuyaInput.GetButtonDown(OuyaButton.DL, p) || Input.GetButtonDown("DL");
                break;
            case GenericButton.DR:
                return OuyaInput.GetButtonDown(OuyaButton.DR, p) || Input.GetButtonDown("DR");
                break;
            case GenericButton.START:
                return OuyaInput.GetButtonDown(OuyaButton.START, p) || Input.GetButtonDown("START");
                break;
            case GenericButton.SELECT:
                return OuyaInput.GetButtonDown(OuyaButton.SELECT, p) || Input.GetButtonDown("SELECT");
                break;
            case GenericButton.SYSTEM:
                return OuyaInput.GetButtonDown(OuyaButton.SYSTEM, p) || Input.GetButtonDown("SYSTEM");
                break;
            default:
                return false;
                break;
        }
    }

    public static bool GetButtonUp(GenericButton button, OuyaPlayer p = OuyaPlayer.P01)
    {
        switch (button)
        {
            case GenericButton.O:
                return OuyaInput.GetButtonUp(OuyaButton.O, p) || Input.GetButtonUp("O");
                break;
            case GenericButton.U:
                return OuyaInput.GetButtonUp(OuyaButton.U, p) || Input.GetButtonUp("U");
                break;
            case GenericButton.Y:
                return OuyaInput.GetButtonUp(OuyaButton.Y, p) || Input.GetButtonUp("Y");
                break;
            case GenericButton.A:
                return OuyaInput.GetButtonUp(OuyaButton.A, p) || Input.GetButtonUp("A");
                break;
            case GenericButton.LB:
                return OuyaInput.GetButtonUp(OuyaButton.LB, p) || Input.GetButtonUp("LB");
                break;
            case GenericButton.RB:
                return OuyaInput.GetButtonUp(OuyaButton.RB, p) || Input.GetButtonUp("RB");
                break;
            case GenericButton.L3:
                return OuyaInput.GetButtonUp(OuyaButton.L3, p) || Input.GetButtonUp("L3");
                break;
            case GenericButton.R3:
                return OuyaInput.GetButtonUp(OuyaButton.R3, p) || Input.GetButtonUp("R3");
                break;
            case GenericButton.LT:
                return OuyaInput.GetButtonUp(OuyaButton.LT, p) || Input.GetButtonUp("LT");
                break;
            case GenericButton.RT:
                return OuyaInput.GetButtonUp(OuyaButton.RT, p) || Input.GetButtonUp("RT");
                break;
            case GenericButton.DU:
                return OuyaInput.GetButtonUp(OuyaButton.DU, p) || Input.GetButtonUp("DU");
                break;
            case GenericButton.DD:
                return OuyaInput.GetButtonUp(OuyaButton.DD, p) || Input.GetButtonUp("DD");
                break;
            case GenericButton.DL:
                return OuyaInput.GetButtonUp(OuyaButton.DL, p) || Input.GetButtonUp("DL");
                break;
            case GenericButton.DR:
                return OuyaInput.GetButtonUp(OuyaButton.DR, p) || Input.GetButtonUp("DR");
                break;
            case GenericButton.START:
                return OuyaInput.GetButtonUp(OuyaButton.START, p) || Input.GetButtonUp("START");
                break;
            case GenericButton.SELECT:
                return OuyaInput.GetButtonUp(OuyaButton.SELECT, p) || Input.GetButtonUp("SELECT");
                break;
            case GenericButton.SYSTEM:
                return OuyaInput.GetButtonUp(OuyaButton.SYSTEM, p) || Input.GetButtonUp("SYSTEM");
                break;
            default:
                return false;
                break;
        }
    }

    public static bool GetButton(GenericButton button, OuyaPlayer p = OuyaPlayer.P01)
    {
        switch (button)
        {
            case GenericButton.O:
                return OuyaInput.GetButton(OuyaButton.O, p) || Input.GetButton("O");
                break;
            case GenericButton.U:
                return OuyaInput.GetButton(OuyaButton.U, p) || Input.GetButton("U");
                break;
            case GenericButton.Y:
                return OuyaInput.GetButton(OuyaButton.Y, p) || Input.GetButton("Y");
                break;
            case GenericButton.A:
                return OuyaInput.GetButton(OuyaButton.A, p) || Input.GetButton("A");
                break;
            case GenericButton.LB:
                return OuyaInput.GetButton(OuyaButton.LB, p) || Input.GetButton("LB");
                break;
            case GenericButton.RB:
                return OuyaInput.GetButton(OuyaButton.RB, p) || Input.GetButton("RB");
                break;
            case GenericButton.L3:
                return OuyaInput.GetButton(OuyaButton.L3, p) || Input.GetButton("L3");
                break;
            case GenericButton.R3:
                return OuyaInput.GetButton(OuyaButton.R3, p) || Input.GetButton("R3");
                break;
            case GenericButton.LT:
                return OuyaInput.GetButton(OuyaButton.LT, p) || Input.GetButton("LT");
                break;
            case GenericButton.RT:
                return OuyaInput.GetButton(OuyaButton.RT, p) || Input.GetButton("RT");
                break;
            case GenericButton.DU:
                return OuyaInput.GetButton(OuyaButton.DU, p) || Input.GetButton("DU");
                break;
            case GenericButton.DD:
                return OuyaInput.GetButton(OuyaButton.DD, p) || Input.GetButton("DD");
                break;
            case GenericButton.DL:
                return OuyaInput.GetButton(OuyaButton.DL, p) || Input.GetButton("DL");
                break;
            case GenericButton.DR:
                return OuyaInput.GetButton(OuyaButton.DR, p) || Input.GetButton("DR");
                break;
            case GenericButton.START:
                return OuyaInput.GetButton(OuyaButton.START, p) || Input.GetButton("START");
                break;
            case GenericButton.SELECT:
                return OuyaInput.GetButton(OuyaButton.SELECT, p) || Input.GetButton("SELECT");
                break;
            case GenericButton.SYSTEM:
                return OuyaInput.GetButton(OuyaButton.SYSTEM, p) || Input.GetButton("SYSTEM");
                break;
            default:
                return false;
                break;
        }
    }
}