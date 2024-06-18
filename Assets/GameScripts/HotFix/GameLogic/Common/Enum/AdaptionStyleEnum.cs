namespace GameLogic.Common 
{
    public enum AdaptionStyle
    {
        /// <summary>
        /// 什么都不做，不改变视频自适应尺寸
        /// </summary>
        None = 0,
        /// <summary>
        /// 保持背景分辨比例不变的情况下强制全屏，可能会阉割掉上下或左右部分视频内容
        /// </summary>
        FullScreen = 1,
        /// <summary>
        /// 允许上下黑边，但永远不会有左右黑边的自适应方式
        /// </summary>
        Letterbox = 2,
    }
}

