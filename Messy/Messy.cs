using System;
using SplashKitSDK;
using SplashKitSDK.Internal;
using SplashKitSDK.Bitmap;
using SplashKitSDK.Window;
using SplashKitSDK.Bitmap;


namespace CharacterDrawing1
{
    public class Program
    {
        public static void Main()
        {
            SpaceGame game = new SpaceGame();
            game.Start();
        }
    }

    public class SpaceGame
    {
        private Thingy _thingy;
        private Window _gameWindow;

        public SpaceGame()
        {
            Initialize();
            _thingy = new Thingy { X = 110, Y = 110 };
        }

        private void Initialize()
        {
            SplashKit.LoadBitmap("Bullet", "./Resources/images/Aquarii.png");
            SplashKit.LoadBitmap("Gliese", "./Resources/images/Gliese.png");
            SplashKit.LoadBitmap("Pegasi", "./Resources/images/Pegasi.png");
            SplashKit.LoadBitmap("Aquarii", "./Resources/images/Fire.png");
        }

        public void Start()
        {
            _gameWindow = new Window("BlastOff", 600, 600);
            while (!_gameWindow.CloseRequested)
            {
                SplashKit.ProcessEvents();
                HandleInput();
                Update();
                Draw();
            }
            _gameWindow.Close();
            _gameWindow = null;
        }

        private void HandleInput()
        {
            if (SplashKit.KeyDown(KeyCode.UpKey))
            {
                _thingy.Move(4, 0);
            }
            if (SplashKit.KeyDown(KeyCode.DownKey))
            {
                _thingy.Move(-4, 0);
            }
            if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                _thingy.Rotate(-4);
            }
            if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                _thingy.Rotate(4);
            }
            if (SplashKit.KeyTyped(KeyCode.SpaceKey))
            {
                _thingy.Shoot();
            }
        }

        private void Update()
        {
            _thingy.Update();
        }

        private void Draw()
        {
            _gameWindow.Clear(Color.Black);
            _thingy.Draw();
            _gameWindow.Refresh(60);
        }
    }

    public class Thingy
    {
        private double _x, _y;
        private double _angle;
        private Bitmap _shipBitmap;
        private Bullet _bullet;

        public Thingy()
        {
            _angle = 270;
            _shipBitmap = SplashKit.BitmapNamed("Aquarii");
        }

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public double Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        public void Rotate(double amount)
        {
            _angle = (_angle + amount) % 360;
        }

        public void Draw()
        {
            _shipBitmap.Draw(_x, _y, SplashKit.OptionRotateBmp(_angle));
            _bullet?.Draw();
        }

        public void Shoot()
        {
            Matrix2D anchorMatrix = SplashKit.TranslationMatrix(SplashKit.PointAt(_shipBitmap.Width / 2, _shipBitmap.Height / 2));
            Matrix2D result = SplashKit.MatrixMultiply(SplashKit.IdentityMatrix(), SplashKit.MatrixInverse(anchorMatrix));
            result = SplashKit.MatrixMultiply(result, SplashKit.RotationMatrix(_angle));
            result = SplashKit.MatrixMultiply(result, anchorMatrix);
            result = SplashKit.MatrixMultiply(result, SplashKit.TranslationMatrix(X, Y));
            Vector2D vector = new Vector2D { X = _shipBitmap.Width, Y = _shipBitmap.Height / 2 };
            vector = SplashKit.MatrixMultiply(result, vector);
            _bullet = new Bullet(vector.X, vector.Y, Angle);
        }

        public void Update()
        {
            _bullet?.Update();
        }

        public void Move(double amountForward, double amountStrafe)
        {
            Vector2D movement = new Vector2D { X = amountForward, Y = amountStrafe };
            Matrix2D rotation = SplashKit.RotationMatrix(_angle);
            movement = SplashKit.MatrixMultiply(rotation, movement);
            _x += movement.X;
            _y += movement.Y;
        }
    }

    public class Bullet
    {
        private Bitmap _bulletBitmap;
        private double _x, _y, _angle;
        private bool _active;

        public Bullet(double x, double y, double angle)
        {
            _bulletBitmap = SplashKit.BitmapNamed("Bullet");
            _x = x - _bulletBitmap.Width / 2;
            _y = y - _bulletBitmap.Height / 2;
            _angle = angle;
            _active = true;
        }

        public Bullet()
        {
            _active = false;
        }

        public void Update()
        {
            const int TOAST = 8;
            Vector2D movement = new Vector2D();
            Matrix2D rotation = SplashKit.RotationMatrix(_angle + 45 * 2);
            movement.X += TOAST;
            movement = SplashKit.MatrixMultiply(rotation, movement);
            _x += movement.X;
            _y += movement.Y;
            if (_x > SplashKit.ScreenWidth() || _x < 0 || _y > SplashKit.ScreenHeight() || _y < 0)
            {
                _active = false;
            }
        }

        public void Draw()
        {
            if (_active)
            {
                DrawingOptions options = SplashKit.OptionRotateBmp(_angle);
                _bulletBitmap.Draw(_x, _y, options);
            }
        }
    }
}
