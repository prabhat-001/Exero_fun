using UnityEngine;
using System.Collections;
using System;
using Windows.Kinect;
public class runjump : MonoBehaviour
{

    private KinectSensor _Sensor;
    private BodyFrameReader _Reader;
    private Body[] _Data = null;
    bool isRunning = false;
    bool jumpleft = false;
    bool jumpright = false;
    public float referencex = 0;
    //static
    Vector3 leftElbowcurrentPosition = Vector3.zero;
    Vector3 leftElbowlastPosition = Vector3.zero;

    Vector3 rightElbowcurrentPosition = Vector3.zero;
    Vector3 rightElbowlastPosition = Vector3.zero;

    Vector3 leftKneecurrentPosition = Vector3.zero;
    Vector3 leftKneelastPosition = Vector3.zero;

    Vector3 rightKneecurrentPosition = Vector3.zero;
    Vector3 rightKneelastPosition = Vector3.zero;

    Vector3 leftAnklecurrentPosition = Vector3.zero;
    Vector3 leftAnklelastPosition = Vector3.zero;

    Vector3 rightAnklecurrentPosition = Vector3.zero;
    Vector3 rightAnklelastPosition = Vector3.zero;

    Vector3 HandLeftcurrentPosition = Vector3.zero;
    Vector3 HandLeftlastPosition = Vector3.zero;

    Vector3 HandRightcurrentPosition = Vector3.zero;
    Vector3 HandRightlastPosition = Vector3.zero;

    Vector3 SpineBasecurrentPosition = Vector3.zero;
    Vector3 SpineBaselastPosition = Vector3.zero;

    Vector3 SpineMidcurrentPosition = Vector3.zero;
    Vector3 SpineMidlastPosition = Vector3.zero;

    Vector3 HipLeftcurrentPosition = Vector3.zero;
    Vector3 HipLeftlastPosition = Vector3.zero;

    Vector3 HipRightcurrentPosition = Vector3.zero;
    Vector3 HipRightlastPosition = Vector3.zero;

    Vector3 ShoulderLeftcurrentPosition = Vector3.zero;
    Vector3 ShoulderLeftlastPosition = Vector3.zero;

    Vector3 ShoulderRightcurrentPosition = Vector3.zero;
    Vector3 ShoulderRightlastPosition = Vector3.zero;




    Vector3 leftElbowdelta = Vector3.zero;
    Vector3 rightElbowdelta = Vector3.zero;
    Vector3 leftKneedelta = Vector3.zero;
    Vector3 rightKneedelta = Vector3.zero;
    Vector3 leftAnkledelta = Vector3.zero;
    Vector3 rightAnkledelta = Vector3.zero;
    Vector3 HandLeftdelta = Vector3.zero;
    Vector3 HandRightdelta = Vector3.zero;
    Vector3 SpineBasedelta = Vector3.zero;
    Vector3 SpineMiddelta = Vector3.zero;
    Vector3 HipLeftdelta = Vector3.zero;
    Vector3 HipRightdelta = Vector3.zero;
    Vector3 ShoulderLeftdelta = Vector3.zero;
    Vector3 ShoulderRightdelta = Vector3.zero;



    public static float[] DeltaData = new float[24];
    public static float[] Deltajump = new float[6];


    //Vector3 RdeltaPos = Vector3.zero;
    //Vector3 LdeltaPos = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        _Sensor = KinectSensor.GetDefault();

        if (_Sensor != null)
        {
            _Reader = _Sensor.BodyFrameSource.OpenReader();

            if (!_Sensor.IsOpen)
            {
                _Sensor.Open();
            }
        }
        
 }
    void OnApplicationQuit()
    {
        if (_Reader != null)
        {
            _Reader.Dispose();
            _Reader = null;
        }

        if (_Sensor != null)
        {
            if (_Sensor.IsOpen)
            {
                _Sensor.Close();
            }
            _Sensor = null;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (_Reader != null)
        {
            var frame = _Reader.AcquireLatestFrame();

            if (frame != null)
            {
                if (_Data == null)
                {
                    _Data = new Body[_Sensor.BodyFrameSource.BodyCount];
                }

                frame.GetAndRefreshBodyData(_Data);

                frame.Dispose();
                frame = null;

                int idx = -1;
                for (int i = 0; i < _Sensor.BodyFrameSource.BodyCount; i++)
                {
                    if (_Data[i].IsTracked)
                    {
                        idx = i;
                    }
                }
                if (idx > -1)
                {
                    //static
                    leftElbowcurrentPosition.x = _Data[idx].Joints[JointType.ElbowLeft].Position.X;
                    leftElbowcurrentPosition.y = _Data[idx].Joints[JointType.ElbowLeft].Position.Y;
                    leftElbowcurrentPosition.z = _Data[idx].Joints[JointType.ElbowLeft].Position.Z;

                    rightElbowcurrentPosition.x = _Data[idx].Joints[JointType.ElbowRight].Position.X;
                    rightElbowcurrentPosition.y = _Data[idx].Joints[JointType.ElbowRight].Position.Y;
                    rightElbowcurrentPosition.z = _Data[idx].Joints[JointType.ElbowRight].Position.Z;

                    HandLeftcurrentPosition.x = _Data[idx].Joints[JointType.HandLeft].Position.X;
                    HandLeftcurrentPosition.y = _Data[idx].Joints[JointType.HandLeft].Position.Y;
                    HandLeftcurrentPosition.z = _Data[idx].Joints[JointType.HandLeft].Position.Z;

                    HandRightcurrentPosition.x = _Data[idx].Joints[JointType.HandRight].Position.X;
                    HandRightcurrentPosition.y = _Data[idx].Joints[JointType.HandRight].Position.Y;
                    HandRightcurrentPosition.z = _Data[idx].Joints[JointType.HandRight].Position.Z;

                    leftKneecurrentPosition.x = _Data[idx].Joints[JointType.KneeLeft].Position.X;
                    leftKneecurrentPosition.y = _Data[idx].Joints[JointType.KneeLeft].Position.Y;
                    leftKneecurrentPosition.z = _Data[idx].Joints[JointType.KneeLeft].Position.Z;

                    rightKneecurrentPosition.x = _Data[idx].Joints[JointType.KneeRight].Position.X;
                    rightKneecurrentPosition.y = _Data[idx].Joints[JointType.KneeRight].Position.Y;
                    rightKneecurrentPosition.z = _Data[idx].Joints[JointType.KneeRight].Position.Z;

                    leftAnklecurrentPosition.x = _Data[idx].Joints[JointType.AnkleLeft].Position.X;
                    leftAnklecurrentPosition.y = _Data[idx].Joints[JointType.AnkleLeft].Position.Y;
                    leftAnklecurrentPosition.z = _Data[idx].Joints[JointType.AnkleLeft].Position.Z;

                    rightAnklecurrentPosition.x = _Data[idx].Joints[JointType.AnkleRight].Position.X;
                    rightAnklecurrentPosition.y = _Data[idx].Joints[JointType.AnkleRight].Position.Y;
                    rightAnklecurrentPosition.z = _Data[idx].Joints[JointType.AnkleRight].Position.Z;

                    //jumping
                    ShoulderRightcurrentPosition.x = _Data[idx].Joints[JointType.ShoulderRight].Position.X;
                    ShoulderRightcurrentPosition.y = _Data[idx].Joints[JointType.ShoulderRight].Position.Y;
                    ShoulderRightcurrentPosition.z = _Data[idx].Joints[JointType.ShoulderRight].Position.Z;

                    ShoulderLeftcurrentPosition.x = _Data[idx].Joints[JointType.ShoulderLeft].Position.X;
                    ShoulderLeftcurrentPosition.y = _Data[idx].Joints[JointType.ShoulderLeft].Position.Y;
                    ShoulderLeftcurrentPosition.z = _Data[idx].Joints[JointType.ShoulderLeft].Position.Z;

                    SpineMidcurrentPosition.x = _Data[idx].Joints[JointType.SpineMid].Position.X;
                    SpineMidcurrentPosition.y = _Data[idx].Joints[JointType.SpineMid].Position.Y;
                    SpineMidcurrentPosition.z = _Data[idx].Joints[JointType.SpineMid].Position.Z;

                    SpineBasecurrentPosition.x = _Data[idx].Joints[JointType.SpineBase].Position.X;
                    SpineBasecurrentPosition.y = _Data[idx].Joints[JointType.SpineBase].Position.Y;
                    SpineBasecurrentPosition.z = _Data[idx].Joints[JointType.SpineBase].Position.Z;

                    HipRightcurrentPosition.x = _Data[idx].Joints[JointType.HipRight].Position.X;
                    HipRightcurrentPosition.y = _Data[idx].Joints[JointType.HipRight].Position.Y;
                    HipRightcurrentPosition.z = _Data[idx].Joints[JointType.HipRight].Position.Z;

                    HipLeftcurrentPosition.x = _Data[idx].Joints[JointType.HipLeft].Position.X;
                    HipLeftcurrentPosition.y = _Data[idx].Joints[JointType.HipLeft].Position.Y;
                    HipLeftcurrentPosition.z = _Data[idx].Joints[JointType.HipLeft].Position.Z;

                    //static
                    leftElbowdelta = leftElbowcurrentPosition - leftElbowlastPosition;
                    rightElbowdelta = rightElbowcurrentPosition - rightElbowlastPosition;
                    leftKneedelta = leftKneecurrentPosition - leftKneelastPosition;
                    rightKneedelta = rightKneecurrentPosition - rightKneelastPosition;
                    leftAnkledelta = leftAnklecurrentPosition - leftAnklelastPosition;
                    rightAnkledelta = rightAnklecurrentPosition - rightAnklelastPosition;
                    HandLeftdelta = HandLeftcurrentPosition - HandLeftlastPosition;
                    HandRightdelta = HandRightcurrentPosition - HandRightlastPosition;
                    //jump
                    ShoulderLeftdelta = ShoulderLeftcurrentPosition - ShoulderLeftlastPosition;
                    ShoulderRightdelta = ShoulderRightcurrentPosition - ShoulderRightcurrentPosition;

                    SpineMiddelta = SpineMidcurrentPosition - SpineMidlastPosition;
                    SpineBasedelta = SpineBasecurrentPosition - SpineBaselastPosition;
                    HipLeftdelta = HipLeftcurrentPosition - HipLeftlastPosition;
                    HipRightdelta = HipRightcurrentPosition - HipRightlastPosition;




                    int i = 0;
                    for (i = 0; i < 10000; i++)
                    {
                        if (i == 9999)
                        {

                            DeltaData[0] = leftElbowdelta.x;// Logging the delta value between  every nth frame.
                            DeltaData[1] = leftElbowdelta.y;
                            DeltaData[2] = leftElbowdelta.z;
                            DeltaData[3] = rightElbowdelta.x;
                            DeltaData[4] = rightElbowdelta.y;
                            DeltaData[5] = rightElbowdelta.z;
                            DeltaData[6] = leftKneedelta.x;// Logging the delta value between  every nth frame.
                            DeltaData[7] = leftKneedelta.y;
                            DeltaData[8] = leftKneedelta.z;
                            DeltaData[9] = rightKneedelta.x;
                            DeltaData[10] = rightKneedelta.y;
                            DeltaData[11] = rightKneedelta.z;
                            DeltaData[12] = leftAnkledelta.x;// Logging the delta value between  every nth frame.
                            DeltaData[13] = leftAnkledelta.y;
                            DeltaData[14] = leftAnkledelta.z;
                            DeltaData[15] = rightAnkledelta.x;
                            DeltaData[16] = rightAnkledelta.y;
                            DeltaData[17] = rightAnkledelta.z;
                            DeltaData[18] = HandRightdelta.x;
                            DeltaData[19] = HandRightdelta.y;
                            DeltaData[20] = HandRightdelta.z;
                            DeltaData[21] = HandLeftdelta.x;
                            DeltaData[22] = HandLeftdelta.y;
                            DeltaData[23] = HandLeftdelta.z;
                            Deltajump[0] = ShoulderLeftdelta.x;
                            Deltajump[1] = ShoulderRightdelta.x;
                            Deltajump[2] = SpineMiddelta.x;
                            Deltajump[3] = SpineBasedelta.x;
                            Deltajump[4] = HipLeftdelta.x;
                            Deltajump[5] = HipRightdelta.x;

                        }

                    }
                  //  float sum = 0;
                    float sumx = 0;
                   // float sumHand = 0;
                   /* for (int j = 0; j < 24; j++)
                    {
                        if (j % 3 != 0)
                            sum = sum + Math.Abs(DeltaData[j]);                     
                    }
                    if (sum > 0.008) isRunning = true;
                    else isRunning = false;

                    if (isRunning)
                    {
                       
                        for (int k = 0; k < 6; k++)
                        {
                            sumx += Deltajump[k];
                        }

                        if (sumx >= 0.091)
                        {
                            jumpright = true;
                            //something
                        }
                        else if (sumx <= -0.091)
                        {
                            jumpleft = true;
                            //something
                        }
                        else
                        {
                            jumpright = false;
                            jumpright = false;
                        }
                        if (jumpright || jumpleft)
                        {
                           // this.gameObject.transform.position = new Vector3(2, 2, 0);
                        }
                        //this.gameObject.transform.position = new Vector3(1, 0, 0);
                    }
                    else
                    {
                        //this.gameObject.transform.position = new Vector3(0, 0, 0);
                    }*/

                    for (int k = 0; k < 6; k++)
                    {
                        sumx += Deltajump[k];
                    }

                    if (sumx >= 0.17&& rightAnklecurrentPosition.x - leftAnklecurrentPosition.x>0.4)
                    {
                        jumpright = true;
                       //this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x+1, 0, 0);
                        //something
                    }
                    else if (sumx <= -0.17 && rightAnklecurrentPosition.x - leftAnklecurrentPosition.x>0.4)
                    {
                        jumpleft = true;
                        //this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - 1, 0, 0);
                        //something
                    }
                    else
                    {
                        jumpright = false;
                        jumpleft = false;
                    }
                   // this.gameObject.transform.position = new Vector3(, 0, 0);
                    /*if (Math.Abs(rightElbowcurrentPosition.y - HandRightcurrentPosition.y) + Math.Abs(leftElbowcurrentPosition.y - HandLeftcurrentPosition.y) < .34||( (rightElbowcurrentPosition.y - HandRightcurrentPosition.y)<0|| (leftElbowcurrentPosition.y - HandLeftcurrentPosition.y)<0))
                    {
                        isRunning = true;
                       this.gameObject.transform.position = new Vector3(0, 1, 0);

                    }
                    else {
                        isRunning = false;
                       this.gameObject.transform.position = new Vector3(0, 0, 0);
                    }

                    */
                    //this.gameObject.transform.position = new Vector3(Math.Abs(rightElbowcurrentPosition.y - HandRightcurrentPosition.y) + Math.Abs(leftElbowcurrentPosition.y - HandLeftcurrentPosition.y), 0, 0);
                    leftElbowlastPosition = leftElbowcurrentPosition;
                    rightElbowlastPosition = rightElbowcurrentPosition;
                    leftKneelastPosition = leftKneecurrentPosition;
                    rightKneelastPosition = rightKneecurrentPosition;
                    leftAnklelastPosition = leftAnklecurrentPosition;
                    rightAnklelastPosition = rightAnklecurrentPosition;
                    HandLeftlastPosition = HandLeftcurrentPosition;
                    HandRightlastPosition =HandRightcurrentPosition;
                    SpineBaselastPosition = SpineBasecurrentPosition;
                    SpineMidlastPosition = SpineMidcurrentPosition;
                    ShoulderLeftlastPosition = ShoulderLeftcurrentPosition;
                    ShoulderRightlastPosition = ShoulderRightcurrentPosition;
                    HipLeftlastPosition = HipLeftcurrentPosition;
                    HipRightlastPosition = HipRightcurrentPosition;




                }
            }
        }
    }
}