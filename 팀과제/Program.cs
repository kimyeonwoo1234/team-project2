namespace 팀과제
{
    using System;
    using System.Runtime.CompilerServices;

    class Program
    {
        // 모든 몬스터 리스트
        private static List<Monster> monsterPool = new List<Monster>();
        private static Monster[] monsters;
        static void Main(string[] args)
        {
            GameDataSet();
            GameMenu();
        }

        static void GameDataSet() {
            monsterPool.Add(new Monster("Goblin", 15, 3));
            monsterPool.Add(new Monster("Gnome", 18, 5));
            monsterPool.Add(new Monster("Orc", 25, 6));
        }

        static void GameMenu()
        {
            int choice;

            do
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 전투 시작");
                Console.WriteLine("0. 종료");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ShowStatus();
                            break;
                        case 2:
                            StartBattle();
                            break;
                        case 0:
                            Console.WriteLine("게임을 종료합니다.");
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }

            } while (choice != 0);
        }

        static void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("**상태 보기**");
            Console.WriteLine("Lv. 01");
            Console.WriteLine("Chad (전사)");
            Console.WriteLine("공격력 : 10");
            Console.WriteLine("방어력 : 5");
            Console.WriteLine("체 력 : 100");
            Console.WriteLine("Gold : 1500 G");
            Console.WriteLine("\n0. 뒤로 가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                if (choice == 0)
                {
                    GameMenu();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    ShowStatus();
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                ShowStatus();
            }
        }

        static void StartBattle()
        {
            // 전투 시작 기능 구현

            // 임시로 만든것이니 마음대로 수정하셔도 됩니다.
                Console.Clear();
                // 몬스터 소환 및 표시 기능
                SpawnMonster();
                Console.WriteLine("0. 종료");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice)) {
                    if (choice == 0) {
                        GameMenu();
                    } else {
                        Console.WriteLine("잘못된 입력입니다.");
                        ShowStatus();
                    }
                } else {
                    Console.WriteLine("숫자를 입력해주세요.");
                    ShowStatus();
                }
            }

        static void SpawnMonster() {
            int num = new Random().Next(1, 5);
            monsters = new Monster[num];
            for (int i = 0; i < num; i++) {
                int monsterIndex = new Random().Next(0, monsterPool.Count());
                monsters[i] = (new Monster(monsterPool[monsterIndex].Name, monsterPool[monsterIndex].Health, monsterPool[monsterIndex].Attack));
            }

            foreach (Monster monster in monsters) {
                Console.WriteLine($"이름 : {monster.Name}");
                Console.WriteLine($"체력 : {monster.Health}");
                Console.WriteLine($"공격력 : {monster.Attack}");
                Console.WriteLine();
            }
        }


        class Person
        {
            public int Health { get; set; }

            public Person(int health)
            {
                this.Health = health;
            }
        }
        class Monster {
            public string Name { get; set; }
            public int Health { get; set; }
            public int Attack { get; set; }
            public bool IsDead { get; set; }

            public Monster(string name, int health, int attack) { 
                this.Name = name;
                this.Health = health;
                this.Attack = attack;
            }

            public void TakeDamage(int damage) {
                Health -= damage;
                if (Health <= 0) {
                    IsDead = true;
                    Console.WriteLine($"{Name} 이(가) 죽었습니다.");
                }
            }

            public int AttckDamage() {
                int damage = (new Random().Next(Attack - 1, Attack + 2));
                return damage;
            }
        }
    }
}

