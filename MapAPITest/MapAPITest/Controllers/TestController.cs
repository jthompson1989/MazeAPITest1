using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Mvc;
using MapAPITest.Models;
using System;

namespace MapAPITest.Controllers
{
    enum Moves
    {
        Right,
        Left,
        Up,
        Down,
        None
    };
    public class TestController : BaseAPIController
    {
        [Route("solveMaze")]
        [HttpPost]
        public JsonResult SolveMaze([System.Web.Http.FromBody] string map)
        {
            var result = GetJsonResult();

            Map mapObject = GetMap(map);

            int movement = FindPath(ref mapObject);

            if (movement == -1)
            {
                result.Data = new MapResult() { Steps = movement,
                    Solution = mapObject.ConvertGridToString() };
            }
            else
            {
                result.Data = new MapResult() { Steps = movement,
                    Solution = mapObject.ConvertGridToString() };
            }

            return result;
        }

        private int FindPath(ref Map map)
        {
            int moveAmount = 0;
            List<Moves> moveHistory = new List<Moves>();
            Point destPoint = new Point(map.EndPoint.XValue, map.EndPoint.YValue, 'A');
            try
            {
                while (!map.ReachedDestination)
                {
                    if (map.MoveRight())
                    {
                        moveHistory.Add(Moves.Right);
                        moveAmount++;
                    }
                    else if (map.MoveDown())
                    {
                        moveHistory.Add(Moves.Down);
                        moveAmount++;
                    }
                    else if (map.MoveLeft())
                    {
                        moveHistory.Add(Moves.Left);
                        moveAmount++;
                    }
                    else if (map.MoveUp())
                    {
                        moveHistory.Add(Moves.Up);
                        moveAmount++;
                    }
                    else
                    {
                        //Reverse Last Move
                        Moves lastMove = moveHistory[(moveHistory.Count - 1)];
                        if(lastMove == Moves.Right)
                        {
                            map.MoveLeft(true);
                            moveAmount--;
                        }
                        else if(lastMove == Moves.Down)
                        {
                            map.MoveUp(true);
                            moveAmount--;
                        }
                        else if(lastMove == Moves.Left)
                        {
                            map.MoveRight(true);
                            moveAmount--;
                        }
                        else if(lastMove == Moves.Up)
                        {
                            map.MoveDown(true);
                            moveAmount--;
                        }
                        moveHistory.RemoveAt((moveHistory.Count - 1));
                    }
                }
                return moveAmount;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        private Map GetMap(string map)
        {
            string[] lines = map.Split(new char[] { '\n', '\r' });
            Map mapObject = new Models.Map();

            int rowNumber = 0;
            foreach (var line in lines)
            {
                int columnNumber = 0;
                foreach(var column in line)
                {
                    if(column == 'A')
                    {
                        mapObject.StartingPoint = new Models.Point(columnNumber, rowNumber, column);
                        mapObject.CurrentPoint = new Models.Point(columnNumber, rowNumber, column);
                    }
                    else if(column == 'B')
                    {
                        mapObject.EndPoint = new Models.Point(columnNumber, rowNumber, column);
                    }
                    mapObject.Grid.Add(new Point(columnNumber, rowNumber, column));
                    columnNumber++;
                }
                mapObject.MaximumX = columnNumber;
                rowNumber++;
            }
            mapObject.MaximumY = rowNumber;

            return mapObject;
        }

    }
}
