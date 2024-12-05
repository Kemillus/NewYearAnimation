#define WIDTH 1200
#define HEIGHT 800

#include "graphics.h"

struct Tree
{
   int x;
   int y;
   int size;
};

struct House
{
   int x;
   int y;
   int size;
   int stages;
   int color;
};

struct Cloud
{
   int x;
   int y;
   int size;
   int step;
};

struct Car
{
   int x;
   int y;
   int size;
   int step;
   IMAGE *image;
};

void draw_sky();
void draw_road();
void draw_tree(Tree);
void draw_house(House);
void draw_cloud(Cloud);
void move_cloud(Cloud&);
void draw_car(Car);
void move_car(Car&);
void save();

int main()
{
   initwindow(WIDTH, HEIGHT);
   
   Tree trees[] = {{ 800, 460, 100},
                   { 980, 460, 100},
                   {1100, 460, 100},
                   {1040, 470, 150},
                   { 860, 470, 150},
                   { 920, 480, 200}};
   
   House houses[] = {{100, 460, 110, 9, COLOR(250, 240, 100)},
                     {250, 470, 120, 7, COLOR(200, 190, 240)},
                     {160, 480, 140, 5, COLOR(250, 150, 150)}};
   
   Cloud clouds[] = {{200,  20, 100,  4},
                     {400, 120, 120, -4},
                     {600,  60, 140,  2}};

   Car cars[] = {{1000, 510, 180, -12, loadBMP("image_1.bmp")}, 
                 { 400, 640, 200,  16, loadBMP("image_2.bmp")},
                 { 200, 530, 220,  -8, loadBMP("image_3.bmp")}};
   
   int n_trees = sizeof(trees) / sizeof(Tree);
   int n_houses = sizeof(houses) / sizeof(House);
   int n_clouds = sizeof(clouds) / sizeof(Cloud);
   int n_cars = sizeof(cars) / sizeof(Car);
   
   while (!kbhit())
   {
      draw_sky();
      draw_road();
      for (int i = 0; i < n_trees; i++)
      {
         draw_tree(trees[i]);
      }
      for (int i = 0; i < n_houses; i++)
      {
         draw_house(houses[i]);
      }
      for (int i = 0; i < n_clouds; i++)
      {
         draw_cloud(clouds[i]);
         move_cloud(clouds[i]);
      }
      for (int i = 0; i < n_cars; i++)
      {
         draw_car(cars[i]);
         move_car(cars[i]);
      }
      swapbuffers();
   }
   save();
   closegraph();
   return 0;
}

void draw_sky()
{
   int y_max = 450, c_max = 255;
   
   for (double y = 0; y < y_max; y++)
   {
      int c = (1 - y / y_max) * c_max;
      setcolor(COLOR(c_max - c, c_max - c / 2, c_max));
      line(0, y, WIDTH - 1, y);
   }
}

void draw_road()
{
   setfillstyle(SOLID_FILL, COLOR(250, 220, 180));
   bar(0, 450, WIDTH, 800);
   
   setfillstyle(SOLID_FILL, COLOR(120, 120, 120));
   bar(0, 525, WIDTH, 775);
   
   setfillstyle(SOLID_FILL, COLOR(250, 210, 0));
   bar(0, 645, WIDTH, 655);
   
   setfillstyle(SOLID_FILL, COLOR(180, 180, 180));
   for (int x = 50; x < WIDTH; x += 100)
   {
      bar(x - 10, 745, x, 780);
      bar(x, 495, x + 10, 520);
   }
   
   setfillstyle(SOLID_FILL, COLOR(200, 200, 200));
   bar(0, 740, WIDTH, 745);
   bar(0, 490, WIDTH, 495);
   
   setfillstyle(SOLID_FILL, COLOR(220, 220, 220));
   bar(0, 735, WIDTH, 740);
   bar(0, 485, WIDTH, 490);
}

void draw_tree(Tree tree)
{
   setcolor(COLOR(100, 100, 100));
   
   setfillstyle(SOLID_FILL, COLOR(180, 110, 30));
   bar(tree.x - 0.05 * tree.size, tree.y - 0.25 * tree.size, tree.x + 0.05 * tree.size, tree.y);
   rectangle(tree.x - 0.05 * tree.size, tree.y - 0.25 * tree.size, tree.x + 0.05 * tree.size, tree.y);
   
   setfillstyle(SOLID_FILL, COLOR(80, 170, 20));
   pieslice(tree.x, tree.y - 0.8 * tree.size, 235, 305, 0.6 * tree.size);
   
   setfillstyle(SOLID_FILL, COLOR(150, 200, 30));
   pieslice(tree.x, tree.y - 0.9 * tree.size, 235, 305, 0.5 * tree.size);
   
   setfillstyle(SOLID_FILL, COLOR(180, 230, 40));
   pieslice(tree.x, tree.y - tree.size, 235, 305, 0.4 * tree.size);
}

void draw_house(House house)
{
   int p[6];
   
   setfillstyle(SOLID_FILL, house.color);
   bar(house.x - 0.45 * house.size, house.y - (0.2 * house.stages + 0.3) * house.size,
       house.x + 0.45 * house.size, house.y);
   
   for (int i = 0; i < house.stages; i++)
   {
      for (int j = 0; j < 4; j++)
      {
         p[0] = house.x - (0.25 - 0.2 * j) * house.size;
         p[1] = house.y - (0.4  + 0.2 * i) * house.size;
         p[2] = house.x - (0.35 - 0.2 * j) * house.size;
         p[3] = house.y - (0.3  + 0.2 * i) * house.size;
         
         setcolor(COLOR(110, 210, 250));
         setfillstyle(SOLID_FILL, COLOR(110, 210, 250));
         p[4] = p[2];
         p[5] = p[1];
         fillpoly(3, p);
         
         setcolor(COLOR(0, 150, 200));
         setfillstyle(SOLID_FILL, COLOR(0, 150, 200));
         p[4] = p[0];
         p[5] = p[3];
         fillpoly(3, p);
      }      
   }
}

void draw_cloud(Cloud cloud)
{
   setcolor(WHITE);
   setfillstyle(SOLID_FILL, WHITE);
   
   bar(cloud.x + 0.2  * cloud.size, cloud.y + 0.3 * cloud.size,
       cloud.x + 0.85 * cloud.size, cloud.y + 0.6 * cloud.size);
   
   fillellipse(cloud.x + 0.2  * cloud.size, cloud.y + 0.4  * cloud.size,
               0.2  * cloud.size, 0.2  * cloud.size);
   fillellipse(cloud.x + 0.4  * cloud.size, cloud.y + 0.25 * cloud.size,
               0.25 * cloud.size, 0.25 * cloud.size);
   fillellipse(cloud.x + 0.65 * cloud.size, cloud.y + 0.3  * cloud.size,
               0.2  * cloud.size, 0.2  * cloud.size);
   fillellipse(cloud.x + 0.85 * cloud.size, cloud.y + 0.45 * cloud.size,
               0.15 * cloud.size, 0.15 * cloud.size);
}

void move_cloud(Cloud &cloud)
{
   cloud.x += cloud.step;
   if (cloud.x < 0 || cloud.x + cloud.size > WIDTH)
   {
      cloud.step = -cloud.step;
      cloud.x += cloud.step;
   }
}

void draw_car(Car car)
{
   putimage(car.x, car.y, car.image, TRANSPARENT_PUT, car.size);
}

void move_car(Car &car)
{
   car.x += car.step;
   if (car.x < -car.size && car.step < 0)
   {
      car.x = WIDTH;
   }
   if (car.x > WIDTH && car.step > 0)
   {
      car.x = -car.size;
   }
}

void save()
{
   int width, height;
   IMAGE *output;
   
   width  = getmaxx() + 1;
   height = getmaxy() + 1;
   output = createimage(width, height);
   
   getimage(0, 0, width - 1, height - 1, output);
   saveBMP("output.bmp", output);
   freeimage(output);
}