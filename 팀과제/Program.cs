namespace 팀과제
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Xml.Linq;
    using System.Threading;

    class Program
    {
        // 모든 몬스터 리스트
        private static List<Monster> monsterPool = new List<Monster>();
        private static Monster[] monsters;
        private static Person person;
        static void Main(string[] args)
        {
            GameDataSet();
            GameMenu();
        }

        static void GameDataSet()
        {
            monsterPool.Add(new Monster("Goblin", 15, 3));
            monsterPool.Add(new Monster("Gnome", 18, 5));
            monsterPool.Add(new Monster("Orc", 25, 6));
            person = new Person("chad", 10, 100);
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
                            return;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    choice = 100;
                }

            } while (choice != 0);
        }

        static void ShowStatus()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("**상태 보기**");
                Console.ResetColor();
                Console.WriteLine("Lv. 01");
                Console.WriteLine($"{person.Name} (전사)");
                Console.WriteLine($"공격력 : {person.Attack}");
                Console.WriteLine("방어력 : 5");
                Console.WriteLine($"체 력 : {person.Health}");
                Console.WriteLine("Gold : 1500 G");
                Console.WriteLine("\n0. 뒤로 가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");


                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice != 0)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    choice = 100;
                }
            } while (choice != 0);

        }

        static void StartBattle()
        {
            // 전투 시작 기능 구현
            Person old_person = new Person("a", 1, person.Health);
            int num = new Random().Next(1, 5);
            int m_len;
            monsters = new Monster[num];
            // 임시로 만든것이니 마음대로 수정하셔도 됩니다.
            // 몬스터 소환 및 표시 기능
            monsters = SpawnMonster(num);

            int choice;
            do
            {
                Console.Clear();
                monsters = monsters.Where((source, index) => monsters[index].IsDead != true).ToArray();
                m_len = monsters.Length;

                if (m_len == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Battle!! - Result\n");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Victory\n");
                    Console.ResetColor();
                    Console.WriteLine($"던전에서 몬스터{num}마리를 잡았습니다.");
                    Console.WriteLine($"{person.Name}");
                    Console.WriteLine($"HP {old_person.Health} -> {person.Health}");
                    Console.WriteLine("Enter 입력 시 메인 화면으로 돌아갑니다.");
                    int.TryParse(Console.ReadLine(), out num);
                    return;
                }
                else if (person.Health <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Battle!! - Result\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You Lose\n");
                    Console.ResetColor();
                    Console.WriteLine($"{person.Name}");
                    Console.WriteLine($"HP {old_person.Health} -> {person.Health}");
                    Console.WriteLine("Enter 입력 시 메인 화면으로 돌아갑니다.");
                    int.TryParse(Console.ReadLine(), out num);
                    return;
                }
                foreach (Monster monster in monsters)
                {
                    Console.WriteLine($"이름 : {monster.Name}");
                    Console.WriteLine($"체력 : {monster.Health}");
                    Console.WriteLine($"공격력 : {monster.Attack}");
                    Console.WriteLine();
                }
                for (int i = 0; i < m_len; i++)
                {
                    Console.WriteLine($"{i + 1}. {monsters[i].Name}");
                }
                Console.WriteLine("0. 종료");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (1 <= choice && choice <= m_len)
                    {
                        battle_enemy(monsters[choice - 1]);
                    }
                    else if (choice != 0)
                    {
                        Console.WriteLine("\n제대로 입력해주세요.");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    choice = 100;
                    Thread.Sleep(1000);
                }
            } while (choice != 0);
        }

        static void battle_enemy(Monster monster)
        {
            monster.TakeDamage(person.AttckDamage());
            person.TakeDamage(monster.AttckDamage());
        }

        static Monster[] SpawnMonster(int num)
        {

            monsters = new Monster[num];
            for (int i = 0; i < num; i++)
            {
                int monsterIndex = new Random().Next(0, monsterPool.Count());
                monsters[i] = (new Monster(monsterPool[monsterIndex].Name, monsterPool[monsterIndex].Health, monsterPool[monsterIndex].Attack));
            }
            return monsters;
        }



        class Person
        {
            public string Name { get; }
            public int Health { get; set; }
            public int Attack { get; set; }
            public bool IsDead { get; set; }
            public Person(string Name, int Attack, int health)
            {
                this.Name = Name;
                this.Attack = Attack;
                this.Health = health;
            }

            public void TakeDamage(int damage)
            {
                Health -= damage;
                if (Health <= 0)
                {
                    IsDead = true;
                    Console.WriteLine($"{Name} 이(가) 죽었습니다.");
                    Thread.Sleep(1000);
                }
            }

            public int AttckDamage()
            {
                int damage = (new Random().Next(Attack - 1, Attack + 2));
                return damage;
            }

        }
        class Monster
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int Attack { get; set; }
            public bool IsDead { get; set; }

            public Monster(string name, int health, int attack)
            {
                this.Name = name;
                this.Health = health;
                this.Attack = attack;
            }

            public void TakeDamage(int damage)
            {
                Health -= damage;
                if (Health <= 0)
                {
                    IsDead = true;
                    Console.WriteLine($"{Name} 이(가) 죽었습니다.");
                    Thread.Sleep(1000);
                }
            }

            public int AttckDamage()
            {
                int damage = (new Random().Next(Attack - 1, Attack + 2));
                return damage;
            }
        }
    }
}

