using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace DungeonSlime;

public class Game1 : Core
{
    // The MonoGame logo texture    
    private AnimatedSprite _slime;
    private AnimatedSprite _bat;
    private Vector2 _slimePosition;

    private const float MOVEMENT_SPEED = 5.0f;
    public Game1() : base("Dungeon Slime", 1280, 720, false)
    {

    }


    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Carrega o atlas do arquivo XML
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        _slime = atlas.CreateAnimatedSprite("slime-animation");
        _slime.Scale = new Vector2(4.0f, 4.0f);

        _bat = atlas.CreateAnimatedSprite("bat-animation");
        _bat.Scale = new Vector2(4.0f, 4.0f);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        CheckKeyboardInput();
        CheckGamePadInput();

        // Update animation sprites
        _slime.Update(gameTime);
        _bat.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Begin the sprite batch to prepare for rendering.
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _slime.Draw(SpriteBatch, _slimePosition);
        
        _bat.Draw(SpriteBatch, new Vector2(_slime.Width + 10, 0));
        // Always end the sprite batch when finished.
        SpriteBatch.End();

        base.Draw(gameTime);
    }

    private void CheckKeyboardInput()
    {
        KeyboardState keyboardState = Keyboard.GetState();
        float speed = MOVEMENT_SPEED;

        if (keyboardState.IsKeyDown(Keys.Space))
        {
            speed *= 1.5f;
        }
        if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
        {
           _slimePosition.Y -= speed;
        }
        if (keyboardState.IsKeyDown(Keys.S)|| keyboardState.IsKeyDown(Keys.Down))
        {
           _slimePosition.Y += speed;
        }
        if (keyboardState.IsKeyDown(Keys.A)|| keyboardState.IsKeyDown(Keys.Left))
        {
           _slimePosition.X -= speed;            
        }
        if (keyboardState.IsKeyDown(Keys.D)|| keyboardState.IsKeyDown(Keys.Right))
        {
           _slimePosition.X += speed;            
        }        
    }

    private void CheckGamePadInput()
    {
        GamePadState gamePadState = GamePad.GetState(PlayerIndex.One); // Gamepad sempre depende de indicar qual playerindex

        float speed = MOVEMENT_SPEED;

        if (gamePadState.IsButtonDown(Buttons.A))
        {
            speed *= 1.5f;
            GamePad.SetVibration(PlayerIndex.One, 1.0f, 1.0f);
        }

        if (gamePadState.ThumbSticks.Left != Vector2.Zero)
        {
            _slimePosition.X += gamePadState.ThumbSticks.Left.X * speed;
            _slimePosition.Y -= gamePadState.ThumbSticks.Left.Y * speed; 
        }
        else
        {
            if (gamePadState.IsButtonDown(Buttons.DPadUp))
            {
                _slimePosition.Y -= speed;
            }
            if (gamePadState.IsButtonDown(Buttons.DPadDown))
            {
                _slimePosition.Y += speed;
            }
            if (gamePadState.IsButtonDown(Buttons.DPadLeft))
            {
                _slimePosition.X -= speed;
            }
            if (gamePadState.IsButtonDown(Buttons.DPadRight))
            {
                _slimePosition.X += speed;
            }            
            
        }
    }

}
