using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 如果要讓RL使用神經網路，請實作此介面
/// </summary>
public interface INeuralNetwork
{
    /// <summary>
    /// 順向傳遞取得數值
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    List<double> GetResult(List<double> input);

    /// <summary>
    /// 開始訓練
    /// </summary>
    /// <param name="trainTimes"></param>
    void StartTrain(int trainTimes);

    /// <summary>
    /// 設定訓練資料
    /// </summary>
    /// <param name="inputs"></param>
    /// <param name="realResults"></param>
    void SetTrainingData(List<List<double>> inputs, List<List<double>> realResults);
}
