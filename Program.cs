using System;
using System.Collections;
using System.Collections.Generic;


namespace CSharp05
{

    //일반화: 선언 단계에서는 알 수 없으나 객체 생성 시 자료형이 정해지는 기법.
    class Box<T>
    {
        public T value;
    }

    //인터페이스 (interfase)
    //프로그램 끼리의 약속(일반화시 <T>가 붙는다
    //상속
    interface IDamage
    {
        void Damage(int power);
    }

    class Glass : IDamage    //인터페이스를 상속한다
    {
        public void Destory()
        {
            //유리를 공격시 부서짐
            Console.WriteLine("유리가 부서짐");
        }
        public void Damage(int power)
        {
            Console.WriteLine("유리가 부서짐");
        }
    }

    class Npc
    {
        //NPC는 공격받는 기능이 없다
    }

    class Enemy
    {
        public void TakeDamage(int power)
        {
            Console.WriteLine($"적이{power}의 데미지를 받았다");
        }
        public void Damage(int power)
        {
            Console.WriteLine($"적이{power}의 데미지를 받았다");
        }
    }


    class Person
    {
        public string name;
        public int age;

        public virtual void Talk()
        {

        }

    }
    // person을 상속
    class Baby : Person
    {
        //virtual 가상함수.
        // => 해당 함수는 자신을 상속한 클래스가 재정의 할 수 있다.
        public virtual void Talk()
        {
            Console.WriteLine("옹알이를 한다.");
        }
       
    }
    //Baby을 상속 
    class Child : Baby
    {

        public void Run()
        {
            Console.WriteLine("뛰어간다");

        }
        public override void Talk()
        {
            //base.Talk();    //원래의 내용을 호출한다
            Console.WriteLine("무언가 말을 한다");
        }

       
    }

    //상속 
    //클래스의 네용을 재사용하거나 수정할 수 있다
    class Animal
    {
        class Cat
        {
            public void Feed(string feed)
            {
                Console.WriteLine($"고양이가{feed}를 먹었다");
            }
        }
        class Lion
        {
            public void Feed(string feed)
            {
                Console.WriteLine($"사자가 {feed} 음식을 먹었다");

            }
        }
        class Rabbit
        {
            public void Feed(string feed)
            {

                Console.WriteLine($"토끼가 {feed}을 먹었다");
            }
        }
        class Hippo
        {
            public void feed(string feed)
            {
                Console.WriteLine($"하마가 {feed}를 먹었다");
            }
        }

        class Zookeeper
        {
            List<Animal> animalList;

            public Zookeeper()
            {
                animalList = new List<Animal>();
            }

            public void Manage(Animal animal)
            {
                animalList.Add(animal);
            }

        }
      
    }


    // 내부의 맴버변수
    //포로퍼티(Property)
    class Container : IEnumerable
    {
        private string[] values; // 현재는 NULL상태 

        public int Length
        {
            get
            {
                return values.Length;
            }
           
            
        }

        public object Current => throw new NotImplementedException();

        //인덱서(indexer)
        public string this[int i]
        {
            get
            {
                return values[i];
            }
        
        }

        public Container()
        {
            values = new string[3]; // 3개의 string배열이 있다 
            values[0] = "AAAA";
            values[1] = "BBBB";
            values[2] = "CCCC";
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < values.Length; i++)
                yield return values[i];                                            
        }      
    }


    internal class Program
    {

        static void Main(string[] args)
        {
            #region
            //범위 지정후 clrl +shift +? =주석 처리

             //객체 box는 int값을 하나 가진다.
             Box<int> box = new Box<int>();
             box.value = 10;

             //객채 box2는 string값을 하나 가진다.
             Box<string> box2 = new Box<string>();
             box2.value = "ABCD";

             Glass glass = new Glass();
             Enemy enemy = new Enemy();

             glass.Destory();
             enemy.TakeDamage(5);

             //is : 해당 객체가 인터페이스 혹은 클래스를 상속하고 있는가? (bool)
             if (glass is IDamage)
             {
                 // as : 객체 간의 형 변환
                 IDamage damage = glass as IDamage;
                 damage.Damage(5);

                 //어떠한 객체가 IDamage를 상속하고 있다면 
                 //적어도 IDmage의 내용은 구현하고 있기 때문에 호출할 수 있다.
             }
             List<IDamage> damageList = new List<IDamage>();

             Enemy[] enemys = new Enemy[5];  //Enemy 객체를 담을 수 있는 공간을 5개 만들었다.
             for (int i = 0; i < enemys.Length; i++)     // 5회 반복하면서
                 enemys[i] = new Enemy();    // 실제 Enemy객채를 생성해야한다.

             Glass[] glasses = new Glass[3];
             for (int i = 0; i < glasses.Length; i++)
                 glasses[i] = new Glass();

             Npc[] npcs = new Npc[2];
             for (int i = 0; i < glasses.Length; i++)
                 npcs[i] = new Npc();

             //가정) 광범위 공격으로 10개의 오브젝트가 잡혔다고 가정한다.
             //이중에서 Enemy,Glass는 공격을 받을 수 있고
             //NPC는 공격을 받는 기능이없다
             AddTarget(enemys, damageList);
             AddTarget(glasses, damageList);
             AddTarget(npcs, damageList);

             foreach (IDamage target in damageList)
                 target.Damage(100);

             Baby baby = new Baby();   //아기 객체 추가
             baby.Talk();              //아이가 말을 한다.

             Child child = new Child();
             child.Run();
             child.Talk();

              //상속 관계에서의 업,다운
              Person p = null;
              p.Talk();

              p = new Child();
              p.Talk();
            #endregion

            

            Container container = new Container();
            for(int i = 0; i < container.Length; i++)
            {
                Console.WriteLine($"values[{i}]: {container[i]}");
            }


            //foreach를 사용하기 위해서는 IEumerable이 필요하다
            foreach(string str in container)
            {
                Console.WriteLine(str);
            }
        }

       

      
        
        //(공격)타겟을 추가하겠다
        //대상이 어떤 자료형이건 상관없게 object배열로 받겠다
        static void AddTarget(object[] targets, List<IDamage> list)
        {  
            foreach(object target in targets)       //모든 object 베열을 순회
            {
                if (target is IDamage)              //target이 IDmage(공격받는기능) 상속한다면  
                    list.Add(target as IDamage);    //list에 추가해준다
            }
        }
    }
}

